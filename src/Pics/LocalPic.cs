using System;

namespace Pics
{
    public class LocalPic
    {
        private int version;
        public LocalPic() {
            version = 1;
        }
        public int Version() {
            return version;
        }
    }
}
