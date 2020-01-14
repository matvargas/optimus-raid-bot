using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Common.IO
{
    public class CustomDataReader : BigEndianReader, ICustomDataReader
    {
        public CustomDataReader(byte[] buffer) : base(buffer) { }
        public int ReadVarInt()
        {
            int retVal = 0;
            int proccesed = 0;
            bool full = false;
            while (proccesed < CustomDataConst.INT_SIZE)
            {
                byte tempon = this.ReadByte();
                full = (tempon & CustomDataConst.MASK_10000000) == CustomDataConst.MASK_10000000;
                if (proccesed > 0)
                {
                    retVal = retVal + ((tempon & CustomDataConst.MASK_01111111) << proccesed);
                }
                else
                {
                    retVal = retVal + (tempon & CustomDataConst.MASK_01111111);
                }
                proccesed = proccesed + CustomDataConst.CHUNCK_BIT_SIZE;
                if (!full)
                {
                    return retVal;
                }
            }
            throw new Exception("Too much data");
        }

        public uint ReadVaruhint()
        {
            return (uint)ReadVarInt();
        }

        public short ReadVarShort()
        {
            int retVal = 0;
            int processed = 0;
            bool finished = false;
            while (processed < CustomDataConst.SHORT_SIZE)
            {
                byte tempon = this.ReadByte();
                finished = (tempon & CustomDataConst.MASK_10000000) == CustomDataConst.MASK_10000000;
                if (processed > 0)
                {
                    retVal = retVal + ((tempon & CustomDataConst.MASK_01111111) << processed);
                }
                else
                {
                    retVal = retVal + (tempon & CustomDataConst.MASK_01111111);
                }
                processed = processed + CustomDataConst.CHUNCK_BIT_SIZE;
                if (!finished)
                {
                    if (retVal > CustomDataConst.SHORT_MAX_VALUE)
                    {
                        retVal = retVal - CustomDataConst.UNSIGNED_SHORT_MAX_VALUE;
                    }
                    return (short)retVal;
                }
            }
            throw new Exception("Too much data");
        }

        public ushort ReadVaruhshort()
        {
            return (ushort)ReadVarShort();
        }

        public Types.Int64 ReadVarLong()
        {
            return ReadInt64();
        }

        public Types.UInt64 ReadVaruhlong()
        {
            return ReadUInt64();
        }

        private Types.UInt64 ReadUInt64()
        {
            uint _loc3_ = 0;
            Types.UInt64 _loc2_ = new Types.UInt64();
            uint _loc4_ = 0;
            while (true)
            {
                _loc3_ = ReadByte();
                if (_loc4_ == 28)
                {
                    break;
                }
                if (_loc3_ >= 128)
                {
                    _loc2_.low = (uint)(_loc2_.low) | (byte)((_loc3_ & 127) << (byte)_loc4_);
                    _loc4_ = _loc4_ + 7;
                    continue;
                }
                _loc2_.low = (uint)_loc2_.low | (byte)((byte)_loc3_ << (byte)_loc4_);
                return _loc2_;
            }
            if (_loc3_ >= 128)
            {
                _loc3_ = _loc3_ & 127;
                _loc2_.low = (uint)_loc2_.low | (byte)((byte)_loc3_ << (byte)_loc4_);
                _loc2_.high = (int)(_loc3_ >> 4);
                _loc4_ = 3;
                while (true)
                {
                    _loc3_ = ReadByte();
                    if (_loc4_ < 32)
                    {
                        if (_loc3_ >= 128)
                        {
                            _loc2_.high = (int)((uint)_loc2_.high | (uint)((byte)(_loc3_ & 127) << (byte)_loc4_));
                        }
                        else
                        {
                            break;
                        }
                    }
                    _loc4_ = _loc4_ + 7;
                }
                _loc2_.high = _loc2_.high | (byte)_loc3_ << (byte)_loc4_;
                return _loc2_;
            }
            _loc2_.low = (uint)_loc2_.low | (uint)((byte)_loc3_ << (byte)_loc4_);
            _loc2_.high = (int)(_loc3_ >> 4);
            return _loc2_;
        }
        private Types.Int64 ReadInt64()
        {
            uint _loc3_ = 0;
            Types.Int64 _loc2_ = new Types.Int64();
            uint _loc4_ = 0;
            while (true)
            {
                _loc3_ = ReadByte();
                if (_loc4_ == 28)
                {
                    break;
                }
                if (_loc3_ >= 128)
                {
                    _loc2_.low = _loc2_.low | (uint)((byte)(_loc3_ & 127) << (byte)_loc4_);
                    _loc4_ = _loc4_ + 7;
                    continue;
                }
                _loc2_.low = _loc2_.low | (uint)((byte)_loc3_ << (byte)_loc4_);
                return _loc2_;
            }
            if (_loc3_ >= 128)
            {
                _loc3_ = _loc3_ & 127;
                _loc2_.low = _loc2_.low | (uint)((byte)_loc3_ << (byte)_loc4_);
                _loc2_.high = (uint)_loc3_ >> 4;
                _loc4_ = 3;
                while (true)
                {
                    _loc3_ = ReadByte();
                    if (_loc4_ < 32)
                    {
                        if (_loc3_ >= 128)
                        {
                            _loc2_.high = _loc2_.high | ((_loc3_ & 127u) << (byte)_loc4_);
                        }
                        else
                        {
                            break;
                        }
                    }
                    _loc4_ = _loc4_ + 7;
                }
                _loc2_.high = _loc2_.high  |_loc3_ << (byte)_loc4_;
                return _loc2_;
            }
            _loc2_.low = _loc2_.low | (uint)((byte)_loc3_ << (byte)_loc4_);
            _loc2_.high = _loc3_ >> 4;
            return _loc2_;
        }
    }
}