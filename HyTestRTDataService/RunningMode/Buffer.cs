using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyTestRTDataService.RunningMode
{
    /// <summary>
    /// All data saved as double.
    /// </summary>
    public class Buffer
    {
        /*local variable*/
        private double[] _rdataList;
        private double[] _wdataList;    //useless.
        private int      _bufferSize_in;
        private int      _bufferSize_out;
        /*event*/
        public EventHandler<EventArgs> bufferDataRefreshed;
        /*properties*/
        public int BufferSizeInput
        {
            get
            {
                return this._bufferSize_in;
            }
        }
        public int BufferSizeOutput
        {
            get
            {
                return this._bufferSize_out;
            }
        }

        public Buffer(int inputSize, int outputSize)
        {
            this._bufferSize_in = inputSize;
            this._bufferSize_out = outputSize;
            this._rdataList = new double[inputSize];
            this._wdataList = new double[outputSize];
        }

        public void update(int id, double value)
        {
            if (id >= _bufferSize_in) return;

            this._rdataList[id] = value;
        }

        public double get(int id)
        {
            if (id >= _bufferSize_in)
            {
                throw new IndexOutOfRangeException();
            }

            return _rdataList[id];
        }
    }
}
