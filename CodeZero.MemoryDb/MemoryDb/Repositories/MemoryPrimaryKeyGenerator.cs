//  <copyright file="MemoryPrimaryKeyGenerator.cs" project="CodeZero.MemoryDb" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;

namespace CodeZero.MemoryDb.Repositories
{
    public class MemoryPrimaryKeyGenerator<TPrimaryKey>
    {
        private object _lastPk;

        public MemoryPrimaryKeyGenerator()
        {
            InitializeLastPk();
        }

        public TPrimaryKey GetNext()
        {
            lock (this)
            {
                return GetNextPrimaryKey();
            }
        }

        private void InitializeLastPk()
        {
            if (typeof(TPrimaryKey) == typeof(int))
            {
                _lastPk = 0;
            }
            else if (typeof(TPrimaryKey) == typeof(long))
            {
                _lastPk = 0L;
            }
            else if (typeof(TPrimaryKey) == typeof(short))
            {
                _lastPk = (short)0;
            }
            else if (typeof(TPrimaryKey) == typeof(byte))
            {
                _lastPk = (byte)0;
            }
            else if (typeof(TPrimaryKey) == typeof(Guid))
            {
                _lastPk = null;
            }
            else
            {
                throw new CodeZeroException("Unsupported primary key type: " + typeof(TPrimaryKey));
            }
        }

        private TPrimaryKey GetNextPrimaryKey()
        {
            if (typeof(TPrimaryKey) == typeof(int))
            {
                _lastPk = ((int)_lastPk) + 1;
            }
            else if (typeof(TPrimaryKey) == typeof(long))
            {
                _lastPk = ((long)_lastPk) + 1L;
            }
            else if (typeof(TPrimaryKey) == typeof(short))
            {
                _lastPk = (short)(((short)_lastPk) + 1);
            }
            else if (typeof(TPrimaryKey) == typeof(byte))
            {
                _lastPk = (byte)(((byte)_lastPk) + 1);
            }
            else if (typeof(TPrimaryKey) == typeof(Guid))
            {
                _lastPk = Guid.NewGuid();
            }
            else
            {
                throw new CodeZeroException("Unsupported primary key type: " + typeof(TPrimaryKey));
            }

            return (TPrimaryKey)_lastPk;
        }
    }
}
