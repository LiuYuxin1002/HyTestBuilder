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
        private double[] _datapool;
        private int      _bufferSize_in;
        private int      _bufferSize_out;
        private int      _bufferSize;
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
        public int BufferSize
        {
            get
            {
                return this._bufferSize;
            }
        }

        public Buffer(int inputSize, int outputSize)
        {
            this._bufferSize_in = inputSize;
            this._bufferSize_out = outputSize;
            this._bufferSize = inputSize + outputSize;
            this._rdataList = new double[inputSize];
            this._wdataList = new double[outputSize];
            this._datapool = new double[_bufferSize];
        }

        public void Update(int id, double value)
        {
            //if (id >= _bufferSize_in)
            //{
            //    throw new IndexOutOfRangeException("请检查你的角标是否超出数组大小");
            //}

            //this._rdataList[id] = value;

            /*tmp method. it'll be better if buffer devide from input and output.*/
            if (id >= _bufferSize)
            {
                throw new IndexOutOfRangeException("请检查你的角标是否超出数组大小");
            }
            this._datapool[id] = value;
        }

        public double Get(int id)
        {
            if (id >= _bufferSize)
            {
                throw new IndexOutOfRangeException("请检查你的角标是否超出数组大小");
            }

            return _datapool[id];
        }
    }
}
