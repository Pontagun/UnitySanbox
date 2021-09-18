using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSConfigurations
{
    public static byte TSS_START_BYTE = 0xF7;                                                                   // stream_slots_bytes
    public static byte TSS_SET_STREAMING_SLOTS = 0x50;                                                          // stream_slots_bytes
    public static byte TSS_SET_STREAMING_TIMING = 0x52;
    public static byte TSS_START_STREAMING = 0x55;
    public static byte TSS_STOP_STREAMING = 0x56;
    public static byte TSS_GET_SENSOR_MOTION = 0x2D;                                                            // stream_slots_bytes
    public static byte TSS_GET_RAD_PER_SEC_GYROSCOPE = 0x26;                                                    // stream_slots_bytes
    public static byte TSS_GET_CORRETED_LINEAR_ACC_AND_GRAVITY = 0x27;                                          // stream_slots_bytes
    public static byte TSS_GET_CORRETED_COMPASS = 0x28;                                                         // stream_slots_bytes
    public static byte TSS_GET_TARED_ORIENTAITON_AS_QUAT = 0x01;                                                // stream_slots_bytes
    public static byte TSS_NULL = 0xFF;                                                                         // stream_slots_bytes
    public static byte TSS_TARE_CURRENT_ORIENTATION = 0x60;
    public static byte CHECK_SUM = (byte)((TSS_SET_STREAMING_SLOTS + TSS_GET_SENSOR_MOTION + TSS_GET_RAD_PER_SEC_GYROSCOPE + TSS_GET_CORRETED_LINEAR_ACC_AND_GRAVITY +
        TSS_GET_CORRETED_COMPASS + TSS_GET_TARED_ORIENTAITON_AS_QUAT + TSS_NULL + TSS_NULL + TSS_NULL) % 256);  // stream_slots_bytes

    public static byte[] stream_slots_bytes = {TSS_START_BYTE,
                                        TSS_SET_STREAMING_SLOTS,
                                        //TSS_GET_SENSOR_MOTION,                      // Slot0 - 4
										TSS_GET_RAD_PER_SEC_GYROSCOPE,              // Slot1 - 12
										TSS_GET_CORRETED_LINEAR_ACC_AND_GRAVITY,    // Slot2 - 12
										TSS_GET_CORRETED_COMPASS,                   // Slot3 - 12
										TSS_GET_TARED_ORIENTAITON_AS_QUAT,          // Slot4 - 16
										TSS_NULL,                                   // Slot5
										TSS_NULL,                                   // Slot6
										TSS_NULL,                                   // Slot7
                                        TSS_NULL,                                   // Slot8
										CHECK_SUM};


}
