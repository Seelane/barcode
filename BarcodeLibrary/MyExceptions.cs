using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeLibrary
{
    public class IdChangedEventArgs : EventArgs
    {
        public int PreviosId { get; }
        public int CurrentId { get; }
        public IdChangedEventArgs(int previosId, int currentId)
        {
            PreviosId = previosId;
            CurrentId = currentId;
        }
    }



}

