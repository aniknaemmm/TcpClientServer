using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    enum command
    {
        help = 0,
        clear,
        back,
        to,
        ls,
        dir,
        download,
        skip,
        locker,
        ugit,
        NOP,
        ping
    }
    internal static class Enum
    {
        public static readonly List<KeyValuePair<command, string>> commandList;

        static Enum()
        {
            commandList = new List<KeyValuePair<command, string>>
            {
                new KeyValuePair<command,string>(command.help,"--help"),
                new KeyValuePair<command,string>(command.clear,"/clear"),
                new KeyValuePair<command,string>(command.back,"/back"),
                new KeyValuePair<command,string>(command.to,"/to"),
                new KeyValuePair<command,string>(command.ls,"/ls"),
                new KeyValuePair<command,string>(command.dir,"/dir"),
                new KeyValuePair<command,string>(command.download,"/download"),
                new KeyValuePair<command,string>(command.skip,"/skip"),
                new KeyValuePair<command,string>(command.locker,"/locker"),
                new KeyValuePair<command,string>(command.NOP,"/NOP"),
                new KeyValuePair<command,string>(command.ping,"/ping"),
            };

        }
    }
    
}
