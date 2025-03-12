namespace PhanMemQuanLyThiCong.Common.Enums
{
    public enum SDCmd : ushort
    {
        SD_FIND = 1,		//Find SecureDongle
        SD_FIND_NEXT = 2,		//Find next
        SD_OPEN = 3,			//Open SecureDongle
        SD_CLOSE = 4,			//Close SecureDongle
        SD_READ,			//Read SecureDongle
        SD_WRITE,			//Write SecureDongle
        SD_RANDOM,			//Generate random
        SD_SEED,			//Generate seed
        SD_DECREASE = 17,
        SD_WRITE_USERID = 9,	//Read UID
        SD_READ_USERID = 10,	//Read UID
        SD_SET_MODULE = 11,   //Set Module
        SD_CHECK_MODULE = 12,	//Check Module
        SD_CALCULATE1 = 14,	//Calculate1
        SD_CALCULATE2,		//Calculate1
        SD_CALCULATE3,      //Calculate1
        SD_SET_COUNTER_EX = 160,         //Set Counter, Type change from WORD to DWORD
        SD_GET_COUNTER_EX = 161,          //Read counter, Type change from WORD to DWORD
        SD_SET_TIMER_EX = 162,         //Set Timer Unit Clock, Type change from WORD to DWORD
        SD_GET_TIMER_EX = 163,        //Get Timer Unit Code, , Type change from WORD to DWORD
        SD_ADJUST_TIMER_EX = 164,
    }

    public enum SDErrCode : uint
    {
        ERR_SUCCESS = 0,							//No error
        ERR_NO_PARALLEL_PORT = 0x80300001,		//(0x80300001)No parallel port
        ERR_NO_DRIVER,							//(0x80300002)No drive
        ERR_NO_DONGLE,							//(0x80300003)No SecureDongle
        ERR_INVALID_pWORD,					//(0x80300004)Invalid pword
        ERR_INVALID_pWORD_OR_ID,				//(0x80300005)Invalid pword or ID
        ERR_SETID,								//(0x80300006)Set id error
        ERR_INVALID_ADDR_OR_SIZE,				//(0x80300007)Invalid address or size
        ERR_UNKNOWN_COMMAND,					//(0x80300008)Unkown command
        ERR_NOTBELEVEL3,						//(0x80300009)Inner error
        ERR_READ,								//(0x8030000A)Read error
        ERR_WRITE,								//(0x8030000B)Write error
        ERR_RANDOM,								//(0x8030000C)Generate random error
        ERR_SEED,								//(0x8030000D)Generate seed error
        ERR_CALCULATE,							//(0x8030000E)Calculate error
        ERR_NO_OPEN,							//(0x8030000F)The SecureDongle is not opened
        ERR_OPEN_OVERFLOW,						//(0x80300010)Open SecureDongle too more(>16)
        ERR_NOMORE = 17,								//(0x80300011)No more SecureDongle
        ERR_NEED_FIND,							//(0x80300012)Need Find before FindNext
        ERR_DECREASE,							//(0x80300013)Dcrease error
        ERR_AR_BADCOMMAND,						//(0x80300014)Band command
        ERR_AR_UNKNOWN_OPCODE,					//(0x80300015)Unkown op code
        ERR_AR_WRONGBEGIN,						//(0x80300016)There could not be constant in first instruction in arithmetic
        ERR_AR_WRONG_END,						//(0x80300017)There could not be constant in last instruction in arithmetic
        ERR_AR_VALUEOVERFLOW,					//(0x80300018)The constant in arithmetic overflow
        ERR_UNKNOWN = 0x8030ffff,					//(0x8030FFFF)Unkown error
        ERR_RECEIVE_NULL = 0x80300100,			//(0x80300100)Receive null
        ERR_PRNPORT_BUSY = 0x80300101				//(0x80300101)Parallel port busy
    }

    public struct SystemTime
    {
        public ushort wYear;
        public ushort wMonth;
        public ushort wDayOfWeek;
        public ushort wDay;
        public ushort wHour;
        public ushort wMinute;
        public ushort wSecond;
        public ushort wMilliseconds;
    }
}