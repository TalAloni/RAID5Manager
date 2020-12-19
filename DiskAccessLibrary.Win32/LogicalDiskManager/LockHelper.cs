/* Copyright (C) 2014-2020 Tal Aloni <tal.aloni.il@gmail.com>. All rights reserved.
 * 
 * You can redistribute this program and/or modify it under the terms of
 * the GNU Lesser Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 */
using System;
using System.Collections.Generic;
using DiskAccessLibrary.LogicalDiskManager;
using DiskAccessLibrary.LogicalDiskManager.Win32;
using DiskAccessLibrary.Win32;

namespace DiskAccessLibrary.Win32
{
    public partial class LockHelper
    {
        public static LockStatus LockAllOrNone(List<DynamicDisk> disksToLock, List<DynamicVolume> volumesToLock)
        {
            bool success = DiskLockHelper.LockAllOrNone(disksToLock);
            if (!success)
            {
                return LockStatus.CannotLockDisk;
            }

            List<Guid> volumeGuids = DynamicVolumeHelper.GetVolumeGuids(volumesToLock);
            success = LockAllVolumesOrNone(volumeGuids);
            if (!success)
            {
                DiskLockHelper.ReleaseLock(disksToLock);
                return LockStatus.CannotLockVolume;
            }

            return LockStatus.Success;
        }
    }
}
