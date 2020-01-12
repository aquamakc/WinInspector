using System;
using System.Collections.Generic;
using System.Text;

namespace InCore
{
    public class InProtocol
    {
        private static InProtocol _handler;
        private static readonly Object LockObject = new object();

        #region Constants

        private enum ComBytes : byte { SOH = 0x01, STX = 0x02, ETX = 0x03, EOT = 0x04, ACK = 0x06, NAK = 0x15 }

        public enum Answer : byte { NOERR = 0, BADBCC = 1, ERRINDATA = 2, ERRLEN = 3, ERROUTDATA = 4 }

        public enum Params : byte { VOLTA, CURRE, POWEP, FREQU }

        #endregion

        public static InProtocol GetHandler()
        {
            lock (LockObject)
            {
                if (_handler == null)
                    _handler = new InProtocol();
                return _handler;
            }
        }

        #region Команды счётчика

        public byte[] ConnectCommand(Device dev)
        {
            List<byte> outCommand = new List<byte>();
            outCommand.AddRange(Encoding.Default.GetBytes("/?"));
            outCommand.AddRange(Encoding.Default.GetBytes(dev.Adress));
            outCommand.AddRange(Encoding.Default.GetBytes("!\r\n"));
            return outCommand.ToArray();
        }

        ///<summary>
        ///Запрос на согласование скоростей
        ///</summary>
        public byte[] SpeedAgreementCommand(Device dev)
        {
            List<byte> outCommand = new List<byte>();
            outCommand.Add((byte)ComBytes.ACK);
            outCommand.AddRange(Encoding.Default.GetBytes($"0{dev.Speed}1\r\n"));
            return outCommand.ToArray();
        }

        ///<summary>
        ///Запрос на авторизацию по паролю
        ///</summary>
        public byte[] PassAgreementCommand(Device dev)
        {
            List<byte> outCommand = new List<byte>();
            outCommand.Add((byte)ComBytes.SOH);
            outCommand.AddRange(Encoding.Default.GetBytes("P1")); // P - команда работы с паролем, 1 - операнд сравнения с внутренним паролем
            outCommand.Add((byte)ComBytes.STX);
            outCommand.AddRange(Encoding.Default.GetBytes($"({dev.Password})"));
            outCommand.Add((byte)ComBytes.ETX);
            outCommand.Add(BccCalc(outCommand.ToArray()));
            return outCommand.ToArray();
        }

        public byte[] GetReadParamCommand(Params needParam)
        {
            List<byte> outCommand = new List<byte>();
            outCommand.Add((byte)ComBytes.SOH);
            outCommand.AddRange(Encoding.Default.GetBytes("R1")); // R - команда чтения, 1 - данные в формате ASCII
            outCommand.Add((byte)ComBytes.STX);
            outCommand.AddRange(Encoding.Default.GetBytes($"{needParam.ToString()}()"));
            outCommand.Add((byte)ComBytes.ETX);
            outCommand.Add(BccCalc(outCommand.ToArray()));
            return outCommand.ToArray();
        }

        #endregion

        #region Разбор ответа счётчика

        ///<summary>
        ///Проверка ответа на начало обмена
        ///</summary>
        public Answer CheckConnectAnswer(byte[] Data, Device dev)
        {
            String inDataStr = Encoding.Default.GetString(Data);
            int strEnd = inDataStr.IndexOf('\r');
            if (strEnd == -1)
                return Answer.ERRINDATA;
            dev.Speed = Encoding.Default.GetString(Data, 4, 1);
            dev.Name = Encoding.Default.GetString(Data, 5, strEnd - 5);
            return Answer.NOERR;
        }

        ///<summary>
        /// Проверка ответа на согласование скоростей
        ///</summary>
        public Answer CheckSpeedAgreement(byte[] Data)
        {
            if (Data.Length == 0)
                return Answer.ERRLEN;
            return CheckBcc(Data) ? Answer.NOERR : Answer.BADBCC;
        }

        public Answer CheckPassAgreement(byte[] Data)
        {
            if (Data.Length != 1)
                return Answer.ERRLEN;
            if (Data[0] == (byte)ComBytes.NAK)
                return Answer.ERROUTDATA;
            if (Data[0] != (byte)ComBytes.ACK)
                return Answer.ERRINDATA;
            return Answer.NOERR;
        }

        public Answer CheckParamCommand (byte[] Data, Device device, Params needParam)
        {
            if (!CheckBcc(Data))
                return Answer.BADBCC;
            String inDataStr = Encoding.Default.GetString(Data);
            if (!inDataStr.Contains(needParam.ToString()) || !inDataStr.Contains("(") || !inDataStr.Contains(")"))
                return Answer.ERRINDATA;
            int s = inDataStr.IndexOf("(") + 1;
            StringBuilder sb = new StringBuilder();
            while (inDataStr[s] != ')')
            {
                sb.Append(inDataStr[s]);
                s++;
            }
            if (!Double.TryParse(sb.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("ru-RU"), out double tmp))
                if (!Double.TryParse(sb.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out tmp))
                    return Answer.ERRINDATA;
            switch(needParam)
            {
                case Params.VOLTA:
                    device.Voltage = tmp;
                    break;
                case Params.CURRE:
                    device.Current = tmp;
                    break;
                case Params.POWEP:
                    device.Power = tmp;
                    break;
                case Params.FREQU:
                    device.Frequency = tmp;
                    break;
                default:
                    return Answer.ERROUTDATA;
            }           
            return Answer.NOERR;
        }

        #endregion

        #region Вспомогательные функции

        ///<summary>
        ///Функция считает контрольную сумму BCC пакета
        ///</summary>
        private byte BccCalc(byte[] Data)
        {
            ushort Bcc = 0; ushort i = 0;
            if (Data[i] == (byte)ComBytes.SOH || Data[i] == (byte)ComBytes.STX) { i++; }
            while (i < Data.Length) { Bcc += Data[i++]; }
            return (byte)(Bcc & 0x7F);
        }

        ///<summary>
        ///Проверка контрольной суммы
        ///</summary>
        private bool CheckBcc(byte[] Data)
        {
            byte[] tmp = new byte[Data.Length - 1];
            Array.Copy(Data, 0, tmp, 0, tmp.Length);
            return BccCalc(tmp) == Data[Data.Length - 1];
        }

        #endregion

    }
}


