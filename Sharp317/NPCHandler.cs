using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sharp317
{
	public class NPCHandler
	{
		public static int teleportToX = -1, teleportToY = -1; // contain absolute x/y
		public static Boolean IsDropping = false;
		public static int maxListedNPCs = 10000, maxNPCs = 10000;
		public static int maxNPCDrops = 10000;
		public static int maxNPCSpawns = 10000;
		public static int[] removeschaos = { 1, 2, 2, 2 };
		public static int[][] worldmap = new int[][]
		{
			new int[]
			{			
				/* 01 */
				3252, 3453, 3252, 3453, 3252, 3253, 3254, 3252, 3253,
				3254, 3255, 3252, 3253, 3252, 3253,
				/* 02 */3248, 3249, 3250, 3251, 3252, 3253, 3254, 3248,
				3249, 3250, 3251, 3252, 3253, 3254, 3248, 3249, 3250, 3251,
				3252, 3254, 3248, 3249, 3250, 3251, 3252, 3253, 3254, 3248,
				3249, 3250, 3251, 3252, 3253, 3254, 3248, 3249, 3250, 3251,
				3252, 3253, 3254, 3248, 3249, 3250, 3251, 3252, 3254, 3248,
				3249, 3250, 3251, 3252, 3253, 3254, 3248, 3249, 3250, 3251,
				3252, 3253, 3254, 3248, 3249, 3250, 3251, 3252, 3253, 3254,
				3248, 3249, 3250, 3251, 3252, 3253, 3254, 3248, 3249, 3250,
				3251, 3252, 3253, 3254, 3248, 3249, 3250, 3251, 3252, 3253,
				3254,
				/* 03 */3235, 3234, 3233, 3232, 3231, 3230, 3235, 3230,
				3235, 3234, 3233, 3232, 3231, 3230, 3234, 3232, 3231, 3234,
				3233, 3232, 3231, 3234, 3233, 3232, 3233, 3231,
				/* 04 */3231, 3230, 3229, 3232, 3231, 3230, 3229, 3229,
				3228, 3227, 3229, 3227, 3232, 3231, 3230, 3229, 3228, 3227,
				3232, 3231, 3230, 3229, 3228, 3227, 3226, 3225, 3232, 3231,
				3230, 3229, 3228, 3227, 3225, 3232, 3231, 3230, 3229, 3228,
				3227, 3225, 3232, 3229, 3228, 3227, 3226, 3232, 3231, 3230,
				3229,
				/* 05 */3217, 3216, 3215, 3214, 3219, 3218, 3217, 3216,
				3219, 3218, 3217, 3219, 3217, 3216, 3215, 3219, 3218, 3217,
				3216, 3215, 3214, 3220, 3219, 3217, 3216, 3215, 3214, 3219,
				3217, 3216, 3215, 3214, 3219, 3217, 3216, 3215, 3214, 3218,
				3217,
				/* 06 */3207, 3206, 3205, 3208, 3207, 3206, 3203, 3207,
				3206, 3205, 3204, 3203, 3207, 3206, 3205, 3204, 3203, 3202,
				3208, 3207, 3206, 3205, 3208, 3207, 3206, 3207,
				/* 07 */3206, 3204, 3203, 3202, 3209, 3208, 3207, 3205,
				3203, 3208, 3207, 3206, 3205, 3203, 3208, 3207, 3206, 3205,
				3204, 3203, 3202, 3208, 3207, 3206, 3205, 3203, 3207, 3206,
				3203, 3206, 3203, 3206, 3205, 3205, 3205,
				/* 08 */3268, 3268, 3268, 3268, 3268, 3269, 3269, 3269,
				3269, 3269, 3270, 3270, 3270, 3270, 3270, 3271, 3271, 3271,
				3271, 3271, 3272, 3272, 3272, 3272, 3272, 3273, 3273, 3273,
				3273, 3273, 3274, 3274, 3274, 3274, 3274, 3275, 3275, 3275,
				3276, 3276, 3276, 3276, 3273, 3274, 3275, 3276, 3273, 3274,
				3275, 3273,
				/* 09 */2958, 2957, 2959, 2958, 2957, 2959, 2958, 2957,
				2959, 2958, 2957, 2956, 2955, 2954, 2953, 2960, 2959, 2956,
				2955, 2953, 2960, 2959, 2957, 2956, 2953,
				/* 10 */2979, 2977, 2976, 2975, 2974, 2973, 2972, 2979,
				2978, 2977, 2972, 2972, 2974, 2973, 2972,
				/* 11 */2952, 2950, 2949, 2948, 2952, 2951, 2950, 2949,
				2948, 2952, 2951, 2950, 2949, 2948, 2952, 2951, 2950, 2949,
				2948, 2952, 2952, 2951,
				/* 12 */2969, 2967, 2966, 2965, 2964, 2963, 2962, 2961,
				2960, 2959, 2958, 2969, 2968, 2967, 2966, 2965, 2964, 2963,
				2962, 2961, 2960, 2959, 2958, 2969, 2968, 2967, 2966, 2965,
				2964, 2963, 2962, 2961, 2960, 2959, 2958, 2969, 2968, 2967,
				2966, 2965, 2964, 2963, 2962, 2961, 2960, 2959, 2958, 2969,
				2968, 2967, 2966, 2965, 2964, 2963, 2962, 2961, 2960, 2959,
				2958, 2969, 2968, 2967, 2966, 2964, 2963, 2962, 2961, 2960,
				2959, 2958, 2969, 2968, 2967, 2966, 2965, 2964, 2963, 2962,
				2961, 2960, 2959, 2958, 2969, 2968, 2967, 2966, 2965, 2964,
				2963, 2962, 2961, 2960, 2959, 2958, 2969, 2968, 2967, 2966,
				2965, 2964, 2963, 2962, 2961, 2960, 2959, 2958,
				/* 13 */2968, 2967, 2966, 2965, 2964, 2963, 2967, 2966,
				2965, 2964, 2966, 2965, 2967, 2966, 2965, 2964, 2968, 2967,
				2966, 2965, 2964, 2963, 2968, 2967, 2966, 2965, 2964, 2963,
				2967, 2966, 2965, 2964, 2968, 2967, 2966, 2965, 2964, 2963,
				/* 14 */3076, 3074, 3076, 3075, 3074, 3077, 3076, 3075,
				3074, 3073, 3077, 3074, 3077, 3076, 3075, 3074,
				/* 15 */3204, 3204, 3203, 3202, 3201, 3204, 3203, 3202,
				3201, 3203, 3201, 3203, 3202, 3201, 3204, 3203, 3201, 3204,
				/* 16 */3315, 3316, 3313, 3314, 3315, 3317, 3318, 3314,
				3317, 3314, 3315, 3316, 3317, 3313, 3314, 3315, 3316, 3317,
				3318, 3314, 3315, 3316, 3317,
				/* 17 */3319, 3320, 3323, 3318, 3319, 3320, 3322, 3323,
				3318, 3319, 3320, 3321, 3322, 3323, 3319, 3320, 3321, 3322,
				3319, 3320, 3322, 3323, 3318, 3319, 3320, 3323, 3319, 3320,
				/* 18 */3315, 3316, 3312, 3313, 3314, 3315, 3316, 3312,
				3313, 3314, 3315, 3316, 3317, 3312, 3313, 3314, 3315, 3316,
				3317, 3318, 3312, 3313, 3314, 3316, 3317, 3312, 3313, 3314,
				3316, 3317, 3312, 3313, 3314, 3316, 3317, 3314, 3317, 3315,
				/* 19 */3314, 3315, 3316, 3318, 3315, 3316, 3317, 3318,
				3314, 3315, 3316, 3317, 3318, 3314, 3315, 3316, 3314, 3315,
				/* 20 */3287, 3288, 3289, 3285, 3286, 3287, 3288, 3289,
				3290, 3287, 3288, 3289, 3290, 3287, 3288, 3289, 3290, 3286,
				3287, 3288, 3287,
				/* 21 */3229, 3232, 3228, 3229, 3230, 3231, 3232, 3233,
				3228, 3230, 3231, 3232, 3233, 3228, 3230, 3231, 3232, 3232,
				/* 22 */3210, 3211, 3209, 3210, 3211, 3212, 3214, 3209,
				3211, 3212, 3213, 3214, 3209, 3211, 3212, 3213, 3209, 3210,
				3211, 3212, 3213, 3214, 3209, 3211, 3212, 3213, 3209, 3210,
				3211, 3212, 3213, 3209, 3211, 3213,
				/* 23 */3026, 3028, 3024, 3025, 3026, 3027, 3028, 3029,
				3025, 3026, 3027, 3028, 3029, 3030, 3024, 3025, 3028, 3029,
				3030, 3024, 3025, 3028, 3029, 3024, 3025, 3026, 3027, 3028,
				3029, 3028, 3029, 3030, 3025, 3026, 3027, 3028, 3029,
				/* 24 */3012, 3013, 3014, 3015, 3016, 3012, 3015, 3016,
				3012, 3015, 3016, 3011, 3012, 3013, 3014, 3015, 3012,
				/* 25 */3012, 3014, 3012, 3013, 3014, 3015, 3012, 3013,
				3014, 3015, 3012, 3013, 3015, 3012, 3013, 3014,
				/* 26 */3013, 3014, 3013, 3014, 3013, 3014, 3015, 3016,
				3012, 3013, 3014, 3015, 3016, 3017, 3012, 3013, 3014, 3015,
				3011, 3012, 3013, 3014, 3015, 3016, 3011, 3012, 3013, 3014,
				3015, 3016, 3011, 3016, 3011, 3013, 3014, 3015, 3016, 3013,
				3014, 3016,
				/* 27 */3012, 3014, 3012, 3013, 3014, 3015, 3016, 3012,
				3015, 3012, 3013, 3014, 3015, 3016, 3013, 3014, 3015, 3013,
				3014, 3013, 3013,
				/* 28 */2946, 2947, 2952, 2946, 2947, 2950, 2951, 2952,
				2946, 2948, 2949, 2950, 2951, 2946, 2948, 2949, 2950, 2951,
				2946, 2947, 2948, 2949, 2950, 2951, 2948, 2949, 2948, 2949,
				/* 29 */2955, 2958, 2959, 2954, 2955, 2956, 2957, 2958,
				2959, 2953, 2954, 2956, 2957, 2958, 2957, 2958, 2959,
				/* 30 */3236, 3236, 3237, 3238, 3239, 3237, 3238, 3239,
				3240, 3236, 3237, 3238, 3239, 3240, 3236, 3237, 3238, 3239,
				3237, 3238,/**/3245, 3246, 3243, 3244, 3245, 3246, 3243,
				3244, 3245, 3246, 3247, 3246, 3247,/**/3261, 3260, 3261,
				3262, 3260, 3261, 3263, 3260, 3263, 3260, 3263, 3260, 3263,
				3260, 3261, 3262, 3263, 3260, 3261, 3263,/**/3234, 3235,
				3238, 3233, 3234, 3235, 3236, 3237, 3238, 3235, 3233, 3234,
				3235, 3236, 3233, 3234, 3235, 3236, 3237, 3238,/**/3290,
				3291, 3292, 3293, 3294, 3297, 3298, 3299, 3290, 3291, 3292,
				3293, 3294, 3295, 3296, 3297, 3298, 3299, 3290, 3291, 3292,
				3293, 3294, 3295, 3296, 3297, 3298, 3299, 3290, 3293, 3294,
				3295, 3296, 3297, 3298, 3299, 3290, 3293, 3294, 3295, 3296,
				3297, 3298, 3299, 3290, 3291, 3292, 3293, 3294, 3295, 3296,
				3297, 3298, 3299, 3290, 3291, 3292, 3293, 3294, 3295, 3296,
				3297, 3298, 3299,
				/* 31 */2662, 2663, 2661, 2662, 2663, 2661, 2662, 2663,
				2661, 2662, 2663, 2662, 2663, 2664, 2665, 2666, 2665, 2666,/**/
				2664, 2665, 2666, 2664, 2665, 2666, 2664, 2665, 2666, 2664,
				2665, 2666, 2664, 2665, 2666,/**/2679, 2680, 2679, 2680,
				2676, 2677, 2678, 2679, 2680, 2676, 2677, 2678, 2679, 2680,
				2676, 2677, 2678, 2679, 2680, 2674, 2675, 2676, 2677, 2678,
				2679, 2680, 2675, 2676, 2677, 2678, 2679, 2680,/**/2667,
				2668, 2669, 2670, 2671, 2667, 2668, 2669, 2670, 2671, 2667,
				2668, 2669, 2670, 2671, 2667, 2668, 2669, 2670, 2671, 2667,
				2668, 2669, 2670, 2671, 2667, 2668, 2669, 2670, 2671, 2667,
				2668, 2669, 2670, 2671, 2667, 2668, 2669, 2670, 2671,/**/
				2681, 2682, 2683, 2684, 2685, 2681, 2682, 2683, 2684, 2685,
				2681, 2682, 2683, 2684, 2685, 2681, 2682, 2683, 2684, 2685,
				2681, 2682, 2683, 2684, 2685,/**/2675, 2676, 2677, 2678,
				2679, 2675, 2676, 2677, 2678, 2679, 2675, 2676, 2677, 2678,
				2679, 2676, 2677, 2678, 2679, 2677, 2678, 2679,/**/
				2672, 2673, 2674, 2675, 2672, 2673, 2674, 2675, 2672, 2673,
				2674, 2675, 2672, 2673, 2674, 2675, 2672, 2673, 2674, 2675,
				2672, 2673, 2674, 2675, 2672, 2673, 2674, 2675,/**/2674,
				2675, 2676, 2677, 2678, 2674, 2675, 2676, 2677, 2678, 2674,
				2675, 2676, 2677, 2678, 2674, 2675, 2676, 2677, 2678, 2674,
				2675, 2677, 2678,/**/2685, 2686, 2687, 2688, 2689, 2685,
				2686, 2687, 2688, 2689, 2685, 2686, 2687, 2688, 2689, 2685,
				2686, 2687, 2688, 2689, 2685, 2686, 2687, 2688, 2689,/**/
				2668, 2669, 2670, 2671, 2672, 2668, 2669, 2670, 2671, 2672,
				2668, 2669, 2670, 2671, 2672, 2668, 2669, 2670, 2671, 2672,
				2668, 2669, 2670, 2671, 2672,/**/2679, 2680, 2681, 2682,
				2683, 2679, 2680, 2681, 2682, 2683, 2679, 2680, 2681, 2682,
				2683, 2679, 2680, 2681, 2682, 2683, 2679, 2680, 2681, 2682,
				2683,/**/2673, 2674, 2675, 2673, 2674, 2675, 2676, 2677,
				2673, 2674, 2675, 2676, 2677, 2673, 2674, 2675, 2676, 2677,
				2673, 2674, 2675, 2676, 2677,/**/2669, 2670, 2671, 2672,
				2669, 2670, 2671, 2672, 2673, 2669, 2670, 2671, 2672, 2673,
				2669, 2670, 2671, 2672, 2673, 2669, 2670, 2671, 2672, 2673,/**/
				2680, 2681, 2682, 2679, 2680, 2681, 2682, 2678, 2679, 2680,
				2681, 2682, 2678, 2679, 2680, 2681, 2682, 2678, 2679, 2680,
				2681, 2682,
				/* 32 */3228, 3229, 3226, 3227, 3228, 3225, 3226, 3228,
				3229, 3226, 3227, 3228, 3229, 3230, 3225, 3226, 3227, 3228,
				3229, 3230, 3225, 3229, 3225, 3226, 3227, 3228, 3229, 3226,/**/
				3232, 3233, 3234, 3235, 3236, 3237, 3232, 3233, 3234, 3235,
				3236, 3231, 3232, 3233, 3234, 3235, 3236, 3227, 3228, 3229,
				3231, 3232, 3233, 3234, 3235, 3236, 3225, 3226, 3227, 3228,
				3229, 3230, 3231, 3233, 3234, 3235, 3236, 3225, 3226, 3227,
				3228, 3229, 3230, 3231, 3232, 3233, 3234, 3235, 3236, 3225,
				3228, 3229, 3230, 3231, 3232, 3235, 3236, 3237, 3225, 3227,
				3228, 3229, 3230, 3231, 3232, 3233, 3235, 3236, 3237, 3225,
				3227, 3228, 3229, 3230, 3231, 3232, 3233, 3235, 3236, 3231,
				3235
			},
			new int[]
			{
				/* 01 */
				3404, 3404, 3403, 3403, 4302, 4302, 4302, 3401, 3401,
				3401, 3401, 3400, 3400, 3399, 3399,
				/* 02 */3398, 3398, 3398, 3398, 3398, 3398, 3398, 3397,
				3397, 3397, 3397, 3397, 3397, 3397, 3396, 3396, 3396, 3396,
				3396, 3396, 3395, 3395, 3395, 3395, 3395, 3395, 3395, 3394,
				3394, 3394, 3394, 3394, 3394, 3394, 3393, 3393, 3393, 3393,
				3393, 3393, 3393, 3392, 3392, 3392, 3392, 3392, 3392, 3391,
				3391, 3391, 3391, 3391, 3391, 3391, 3390, 3390, 3390, 3390,
				3390, 3390, 3390, 3389, 3389, 3389, 3389, 3389, 3389, 3389,
				3388, 3388, 3388, 3388, 3388, 3388, 3388, 3387, 3387, 3387,
				3387, 3387, 3387, 3387, 3386, 3386, 3386, 3386, 3386, 3386,
				3386,
				/* 03 */3421, 3421, 3421, 3421, 3421, 3421, 3422, 3422,
				3423, 3423, 3423, 3423, 3423, 3423, 3424, 3424, 3424, 3425,
				3425, 3425, 3425, 3426, 3426, 3426, 3427, 3427,
				/* 04 */3433, 3433, 3433, 3434, 3434, 3434, 3434, 3435,
				3435, 3435, 3436, 3436, 3437, 3437, 3437, 3437, 3437, 3437,
				3438, 3438, 3438, 3438, 3438, 3438, 3438, 3438, 3439, 3439,
				3439, 3439, 3439, 3439, 3439, 3440, 3440, 3440, 3440, 3440,
				3440, 3440, 3441, 3441, 3441, 3441, 3441, 3442, 3442, 3442,
				3442,
				/* 05 */3411, 3411, 3411, 3411, 3412, 3412, 3412, 3412,
				3413, 3413, 3413, 3414, 3414, 3414, 3414, 3415, 3415, 3415,
				3415, 3415, 3415, 3416, 3416, 3416, 3416, 3416, 3416, 3417,
				3417, 3417, 3417, 3417, 3418, 3418, 3418, 3418, 3418, 3419,
				3419,
				/* 06 */3414, 3414, 3414, 3415, 3415, 3415, 3415, 3416,
				3416, 3416, 3416, 3416, 3417, 3417, 3417, 3417, 3417, 3417,
				3418, 3418, 3418, 3418, 3419, 3419, 3419, 3420,
				/* 07 */3495, 3495, 3495, 3495, 3396, 3396, 3396, 3396,
				3396, 3397, 3397, 3397, 3397, 3397, 3398, 3398, 3398, 3398,
				3398, 3398, 3398, 3399, 3399, 3399, 3399, 3399, 3400, 3400,
				3400, 3401, 3401, 3402, 3402, 3403, 3404,
				/* 08 */3426, 3427, 3428, 3429, 3430, 3426, 3427, 3428,
				3429, 3430, 3426, 3427, 3428, 3429, 3430, 3426, 3427, 3428,
				3429, 3430, 3426, 3427, 3428, 3429, 3430, 3426, 3427, 3428,
				3429, 3430, 3426, 3427, 3428, 3429, 3430, 3227, 3228, 3229,
				3426, 3427, 3430, 3420, 3421, 3421, 3421, 3421, 3422, 3422,
				3422, 3423,
				/* 09 */3385, 3385, 3386, 3386, 3386, 3387, 3387, 3387,
				3388, 3388, 3388, 3388, 3388, 3388, 3388, 3389, 3389, 3389,
				3389, 3389, 3390, 3390, 3390, 3390, 3390,
				/* 10 */3383, 3383, 3383, 3383, 3383, 3383, 3383, 3384,
				3384, 3384, 3384, 3385, 3386, 3386, 3386,
				/* 11 */3385, 3385, 3385, 3385, 3386, 3386, 3386, 3386,
				3386, 3387, 3387, 3387, 3387, 3387, 3388, 3388, 3388, 3388,
				3388, 3389, 3390, 3390,
				/* 12 */3376, 3376, 3376, 3376, 3376, 3376, 3376, 3376,
				3376, 3376, 3376, 3377, 3377, 3377, 3377, 3377, 3377, 3377,
				3377, 3377, 3377, 3377, 3377, 3378, 3378, 3378, 3378, 3378,
				3378, 3378, 3378, 3378, 3378, 3378, 3378, 3379, 3379, 3379,
				3379, 3379, 3379, 3379, 3379, 3379, 3379, 3379, 3379, 3380,
				3380, 3380, 3380, 3380, 3380, 3380, 3380, 3380, 3380, 3380,
				3380, 3381, 3381, 3381, 3381, 3381, 3381, 3381, 3381, 3381,
				3381, 3381, 3382, 3382, 3382, 3382, 3382, 3382, 3382, 3382,
				3382, 3382, 3382, 3382, 3383, 3383, 3383, 3383, 3383, 3383,
				3383, 3383, 3383, 3383, 3383, 3383, 3384, 3384, 3384, 3384,
				3384, 3384, 3384, 3384, 3384, 3384, 3384, 3384,
				/* 13 */3391, 3391, 3391, 3391, 3391, 3391, 3392, 3392,
				3392, 3392, 3393, 3393, 3394, 3394, 3394, 3394, 3395, 3395,
				3395, 3395, 3395, 3395, 3396, 3396, 3396, 3396, 3396, 3396,
				3397, 3397, 3397, 3397, 3398, 3398, 3398, 3398, 3398, 3398,
				/* 14 */3427, 3427, 3428, 3428, 3428, 3429, 3429, 3429,
				3429, 3429, 3430, 3430, 3431, 3431, 3431, 3431,
				/* 15 */3431, 3432, 3432, 3432, 3432, 3433, 3433, 3433,
				3433, 3434, 3434, 3435, 3435, 3435, 3436, 3436, 3436, 3437,
				/* 16 */3160, 3160, 3161, 3161, 3161, 3161, 3161, 3162,
				3162, 3163, 3163, 3163, 3163, 3164, 3164, 3164, 3164, 3164,
				3164, 3165, 3165, 3165, 3165,
				/* 17 */3191, 3191, 3191, 3192, 3192, 3192, 3192, 3192,
				3193, 3193, 3193, 3193, 3193, 3193, 3194, 3194, 3194, 3194,
				3195, 3195, 3195, 3195, 3196, 3196, 3196, 3196, 3197, 3197,
				/* 18 */3178, 3178, 3179, 3179, 3179, 3179, 3179, 3180,
				3180, 3180, 3180, 3180, 3180, 3181, 3181, 3181, 3181, 3181,
				3181, 3181, 3182, 3182, 3182, 3182, 3182, 3183, 3183, 3183,
				3183, 3183, 3184, 3184, 3184, 3184, 3184, 3185, 3185, 3186,
				/* 19 */3173, 3173, 3173, 3173, 3174, 3174, 3174, 3174,
				3175, 3175, 3175, 3175, 3175, 3176, 3176, 3176, 3177, 3177,
				/* 20 */3187, 3187, 3187, 3188, 3188, 3188, 3188, 3188,
				3188, 3189, 3189, 3189, 3189, 3190, 3190, 3190, 3190, 3191,
				3191, 3191, 3192,
				/* 21 */3201, 3201, 3202, 3202, 3202, 3202, 3202, 3202,
				3203, 3203, 3203, 3203, 3203, 3204, 3204, 3204, 3204, 3205,
				/* 22 */3243, 3243, 3244, 3244, 3244, 3244, 3244, 3245,
				3245, 3245, 3245, 3245, 3246, 3246, 3246, 3246, 3247, 3247,
				3247, 3247, 3247, 3247, 3248, 3248, 3248, 3248, 3249, 3249,
				3249, 3249, 3249, 3250, 3250, 3250,
				/* 23 */3245, 3245, 3246, 3246, 3246, 3246, 3246, 3246,
				3247, 3247, 3247, 3247, 3247, 3247, 3248, 3248, 3248, 3248,
				3248, 3249, 3249, 3249, 3249, 3250, 3250, 3250, 3250, 3250,
				3250, 3251, 3251, 3251, 3252, 3252, 3252, 3252, 3252,
				/* 24 */3257, 3257, 3257, 3257, 3257, 3258, 3258, 3258,
				3259, 3259, 3259, 3260, 3260, 3260, 3260, 3260, 3261,
				/* 25 */3244, 3244, 3245, 3245, 3245, 3245, 3246, 3246,
				3246, 3246, 3247, 3247, 3247, 3248, 3248, 3248,
				/* 26 */3220, 3220, 3221, 3221, 3222, 3222, 3222, 3222,
				3223, 3223, 3223, 3223, 3223, 3223, 3224, 3224, 3224, 3224,
				3225, 3225, 3225, 3225, 3225, 3225, 3226, 3226, 3226, 3226,
				3226, 3226, 3227, 3227, 3228, 3228, 3228, 3228, 3228, 3229,
				3229, 3229,
				/* 27 */3203, 3203, 3204, 3204, 3204, 3204, 3204, 3205,
				3205, 3206, 3206, 3206, 3206, 3206, 3207, 3207, 3207, 3208,
				3208, 3209, 3210,
				/* 28 */3202, 3202, 3202, 3203, 3203, 3203, 3203, 3203,
				3204, 3204, 3204, 3204, 3204, 3205, 3205, 3205, 3205, 3205,
				3206, 3206, 3206, 3206, 3206, 3206, 3207, 3207, 3208, 3208,
				/* 29 */3202, 3202, 3202, 3203, 3203, 3203, 3203, 3203,
				3203, 3204, 3204, 3204, 3204, 3204, 3205, 3205, 3205,
				/* 30 */3403, 3404, 3404, 3404, 3404, 3405, 3405, 3405,
				3405, 3406, 3406, 3406, 3406, 3406, 3407, 3407, 3407, 3407,
				3408, 3408,/**/3393, 3393, 3394, 3394, 3394, 3394, 3395,
				3395, 3395, 3395, 3395, 3396, 3396,/**/3396, 3397, 3397,
				3397, 3398, 3398, 3398, 3399, 3399, 3400, 3400, 3401, 3401,
				3402, 3402, 3402, 3402, 3403, 3403, 3403,/**/3382, 3382,
				3382, 3383, 3383, 3383, 3383, 3383, 3383, 3384, 3385, 3385,
				3385, 3385, 3386, 3386, 3386, 3386, 3386, 3386,/**/3377,
				3377, 3377, 3377, 3377, 3377, 3377, 3377, 3378, 3378, 3378,
				3378, 3378, 3378, 3378, 3378, 3378, 3378, 3379, 3379, 3379,
				3379, 3379, 3379, 3379, 3379, 3379, 3379, 3380, 3380, 3380,
				3380, 3380, 3380, 3380, 3380, 3381, 3381, 3381, 3381, 3381,
				3381, 3381, 3381, 3382, 3382, 3382, 3382, 3382, 3382, 3382,
				3382, 3382, 3382, 3383, 3383, 3383, 3383, 3383, 3383, 3383,
				3383, 3383, 3383,
				/* 31 */3713, 3713, 3714, 3714, 3714, 3715, 3715, 3715,
				3716, 3716, 3716, 3717, 3717, 3718, 3718, 3718, 3719, 3719,/**/
				3713, 3713, 3713, 3714, 3714, 3714, 3715, 3715, 3715, 3716,
				3716, 3716, 3717, 3717, 3717,/**/3714, 3714, 3715, 3715,
				3716, 3716, 3716, 3716, 3716, 3717, 3717, 3717, 3717, 3717,
				3718, 3718, 3718, 3718, 3718, 3719, 3719, 3719, 3719, 3719,
				3719, 3719, 3720, 3720, 3720, 3720, 3720, 3720,/**/3712,
				3712, 3712, 3712, 3712, 3713, 3713, 3713, 3713, 3713, 3714,
				3714, 3714, 3714, 3714, 3715, 3715, 3715, 3715, 3715, 3716,
				3716, 3716, 3716, 3716, 3717, 3717, 3717, 3717, 3717, 3718,
				3718, 3718, 3718, 3718, 3719, 3719, 3719, 3719, 3719,/**/
				3714, 3714, 3714, 3714, 3714, 3715, 3715, 3715, 3715, 3715,
				3716, 3716, 3716, 3716, 3716, 3717, 3717, 3717, 3717, 3717,
				3718, 3718, 3718, 3718, 3718,/**/3718, 3718, 3718, 3718,
				3718, 3719, 3719, 3719, 3719, 3719, 3720, 3720, 3720, 3720,
				3720, 3721, 3721, 3721, 3721, 3722, 3722, 3722,/**/
				3712, 3712, 3712, 3712, 3713, 3713, 3713, 3713, 3714, 3714,
				3714, 3714, 3715, 3715, 3715, 3715, 3716, 3716, 3716, 3716,
				3717, 3717, 3717, 3717, 3718, 3718, 3718, 3718,/**/3711,
				3711, 3711, 3711, 3711, 3712, 3712, 3712, 3712, 3712, 3713,
				3713, 3713, 3713, 3713, 3714, 3714, 3714, 3714, 3714, 3715,
				3715, 3715, 3715, 3715,/**/3722, 3722, 3722, 3722, 3722,
				3723, 3723, 3723, 3723, 3723, 3724, 3724, 3724, 3724, 3724,
				3725, 3725, 3725, 3725, 3725, 3726, 3726, 3726, 3726, 3726,/**/
				3725, 3725, 3725, 3725, 3725, 3726, 3726, 3726, 3726, 3726,
				3727, 3727, 3727, 3727, 3727, 3728, 3728, 3728, 3728, 3728,
				3729, 3729, 3729, 3729, 3729,/**/3730, 3730, 3730, 3730,
				3730, 3731, 3731, 3731, 3731, 3731, 3732, 3732, 3732, 3732,
				3732, 3733, 3733, 3733, 3733, 3733, 3734, 3734, 3734, 3734,
				3734,/**/3727, 3727, 3727, 3728, 3728, 3728, 3728, 3728,
				3729, 3729, 3729, 3729, 3729, 3730, 3730, 3730, 3730, 3730,
				3731, 3731, 3731, 3731, 3731,/**/3723, 3723, 3723, 3723,
				3724, 3724, 3724, 3724, 3724, 3725, 3725, 3725, 3725, 3725,
				3726, 3726, 3726, 3726, 3726, 3727, 3727, 3727, 3727, 3727,/**/
				3726, 3726, 3726, 3727, 3727, 3727, 3727, 3728, 3728, 3728,
				3728, 3728, 3729, 3729, 3729, 3729, 3729, 3730, 3730, 3730,
				3730, 3730,
				/* 32 */3287, 3287, 3288, 3288, 3288, 3289, 3289, 3289,
				3289, 3290, 3290, 3290, 3290, 3290, 3291, 3291, 3291, 3291,
				3291, 3291, 3292, 3292, 3293, 3293, 3293, 3293, 3293, 3294,/**/
				3292, 3292, 3292, 3292, 3292, 3292, 3293, 3293, 3293, 3293,
				3293, 3294, 3294, 3294, 3294, 3294, 3294, 3295, 3295, 3295,
				3295, 3295, 3295, 3295, 3295, 3295, 3296, 3296, 3296, 3296,
				3296, 3296, 3296, 3296, 3296, 3296, 3296, 3297, 3297, 3297,
				3297, 3297, 3297, 3297, 3297, 3297, 3297, 3297, 3297, 3298,
				3298, 3298, 3298, 3298, 3298, 3298, 3298, 3298, 3299, 3299,
				3299, 3299, 3299, 3299, 3299, 3299, 3299, 3299, 3299, 3300,
				3300, 3300, 3300, 3300, 3300, 3300, 3300, 3300, 3300, 3301,
				3301
			}
		};

		/*
		 * WORLDMAP 2: (not-walk able places) 01 - Lumbridge cows
		 */
		public static int[][] worldmap2 = new int[][]
		{
			new int[]
			{
				/* 01 */
				3257, 3258, 3260, 3261, 3261, 3262, 3261, 3262, 3257,
				3258, 3257, 3258, 3254, 3255, 3254, 3255, 3252, 3252, 3250,
				3251, 3250, 3251, 3249, 3250, 3249, 3250, 3242, 3242, 3243,
				3243, 3257, 3244, 3245, 3244, 3245, 3247, 3248, 3250, 3251,
				3255, 3256, 3255, 3256, 3259, 3260
			},
			new int[]
			{
				/* 01 */
				3256, 3256, 3256, 3256, 3266, 3266, 3267, 3267, 3270,
				3270, 3271, 3271, 3272, 3272, 3273, 3273, 3275, 3276, 3277,
				3277, 3278, 3278, 3279, 3279, 3280, 3280, 3285, 3286, 3289,
				3290, 3289, 3297, 3297, 3298, 3298, 3298, 3298, 3297, 3297,
				3297, 3297, 3298, 3298, 3297, 3297, 
			}
		};

		public static int randomremoveschaos( )
		{
			return removeschaos[( int ) ( MathHelper.Random() * removeschaos.Length )];
		}

		public int[] combatLevel = new int[3851];
		public int[] dropCount = new int[3851];

		public double[][] drops = new double[3851][];// 45 // room for 15 drops per npc

		public NPCDrops[] NpcDrops = new NPCDrops[maxNPCDrops];

		public NPCList[] NpcList = new NPCList[maxListedNPCs];

		public NPC[] npcs = new NPC[maxNPCSpawns];

		public int remove = 2; // 1 = removes equipment, 2 = doesn't remove -
							   // xerozcheez

		public int[] respawnTime = new int[3851];

		public NPCHandler( )
		{
			for ( var i = 0; i < drops.Length; i++ )
			{
				drops[i] = new double[45];
			}

			for ( int i = 0; i < maxNPCSpawns; i++ )
			{
				npcs[i] = null;
			}
			for ( int i = 0; i < maxListedNPCs; i++ )
			{
				NpcList[i] = null;
			}
			for ( int i = 0; i < maxNPCDrops; i++ )
			{
				NpcDrops[i] = null;
			}
			misc.print( "Loading... " );
			loadNPCList( "npc.cfg" );

			loadNPCDrops( "npcdrops.cfg" );

			loadAutoSpawn( "autospawn.cfg" );
			misc.println( "done " );

			misc.print( "Server Information: " );
			misc.println( " PORT: " + server.serverlistenerPort + "  MAXPLAYERS: " + server.MaxConnections + " " );
		}

		public Boolean AttackNPC( int NPCID )
		{
			if ( server.npcHandler.npcs[npcs[NPCID].attacknpc] != null )
			{
				int EnemyX = server.npcHandler.npcs[npcs[NPCID].attacknpc].absX;
				int EnemyY = server.npcHandler.npcs[npcs[NPCID].attacknpc].absY;
				int EnemyHP = server.npcHandler.npcs[npcs[NPCID].attacknpc].HP;
				int hitDiff = 0;

				hitDiff = misc.random( npcs[NPCID].MaxHit );
				if ( GoodDistance( EnemyX, EnemyY, npcs[NPCID].absX,
						npcs[NPCID].absY, 1 ) == true )
				{
					if ( server.npcHandler.npcs[npcs[NPCID].attacknpc].IsDead == true )
					{
						ResetAttackNPC( NPCID );
						// npcs[NPCID].textUpdate = "Oh yeah I win bitch!";
						// npcs[NPCID].textUpdateRequired = true;
						npcs[NPCID].animNumber = 2103;
						npcs[NPCID].animUpdateRequired = true;
						npcs[NPCID].updateRequired = true;
					}
					else
					{
						if ( ( EnemyHP - hitDiff ) < 0 )
						{
							hitDiff = EnemyHP;
						}
						if ( npcs[NPCID].npcType == 9 )
							npcs[NPCID].animNumber = 386;
						if ( npcs[NPCID].npcType == 2745 )
						{

							if ( misc.random( 2 ) == 2 )
							{
								hitDiff = misc.random( 23 );
							}
							if ( misc.random( 2 ) == 1 )
							{
								hitDiff = 0;
							}
						}
						if ( npcs[NPCID].npcType == 3200 )
							npcs[NPCID].animNumber = 0x326; // drags: chaos ele
															// emote ( YESSS )
						if ( ( npcs[NPCID].npcType == 1605 )
								|| ( npcs[NPCID].npcType == 1472 ) )
						{
							npcs[NPCID].animNumber = 386; // drags: abberant
														  // spector death ( YAY )
						}
						npcs[NPCID].animUpdateRequired = true;
						npcs[NPCID].updateRequired = true;
						server.npcHandler.npcs[npcs[NPCID].attacknpc].hitDiff = hitDiff;
						server.npcHandler.npcs[npcs[NPCID].attacknpc].attacknpc = NPCID;
						server.npcHandler.npcs[npcs[NPCID].attacknpc].updateRequired = true;
						server.npcHandler.npcs[npcs[NPCID].attacknpc].hitUpdateRequired = true;
						npcs[NPCID].actionTimer = 7;
						return true;
					}
				}
			}
			return false;
		}

		public Boolean AttackNPCMage( int NPCID )
		{
			int EnemyX = server.npcHandler.npcs[npcs[NPCID].attacknpc].absX;
			int EnemyY = server.npcHandler.npcs[npcs[NPCID].attacknpc].absY;
			int EnemyHP = server.npcHandler.npcs[npcs[NPCID].attacknpc].HP;
			int hitDiff = 0;

			// hitDiff = misc.random(npcs[NPCID].MaxHit);
			if ( npcs[NPCID].actionTimer == 0 )
			{
				if ( server.npcHandler.npcs[npcs[NPCID].attacknpc].IsDead == true )
				{
					ResetAttackNPC( NPCID );
					// npcs[NPCID].textUpdate = "Oh yeah I win bitch!";
					// npcs[NPCID].textUpdateRequired = true;
					npcs[NPCID].animNumber = 2103;
					npcs[NPCID].animUpdateRequired = true;
					npcs[NPCID].updateRequired = true;
				}
				else
				{
					npcs[NPCID].animNumber = 711; // mage
												  // attack
					if ( npcs[NPCID].npcType == 1645 )
					{
						gfxAll( 369, EnemyY, EnemyX );
						hitDiff = 6 + misc.random( 43 );
					}
					if ( npcs[NPCID].npcType == 1645 )
					{
						gfxAll( 369, EnemyY, EnemyX );
						hitDiff = 6 + misc.random( 43 );
					}
					if ( npcs[NPCID].npcType == 509 )
					{
						gfxAll( 365, EnemyY, EnemyX );
						hitDiff = 8 + misc.random( 51 );
					}
					if ( npcs[NPCID].npcType == 1241 )
					{
						gfxAll( 363, EnemyY, EnemyX );
						hitDiff = 2 + misc.random( 19 );
					}
					if ( npcs[NPCID].npcType == 1246 )
					{
						gfxAll( 368, npcs[NPCID].absY, npcs[NPCID].absX );
						gfxAll( 367, EnemyY, EnemyX );
						hitDiff = 4 + misc.random( 35 );
					}
					npcs[NPCID].animUpdateRequired = true;
					npcs[NPCID].updateRequired = true;
					if ( ( EnemyHP - hitDiff ) < 0 )
					{
						hitDiff = EnemyHP;
					}
					server.npcHandler.npcs[npcs[NPCID].attacknpc].hitDiff = hitDiff;
					server.npcHandler.npcs[npcs[NPCID].attacknpc].attacknpc = NPCID;
					server.npcHandler.npcs[npcs[NPCID].attacknpc].updateRequired = true;
					server.npcHandler.npcs[npcs[NPCID].attacknpc].hitUpdateRequired = true;
					npcs[NPCID].actionTimer = 7;
					return true;
				}
				return false;
			}
			return false;
		}

		public Boolean AttackPlayer( int NPCID )
		{

			if ( npcs[NPCID].getKiller() == 0 )
			{

				return false;
			}
			int Player = npcs[NPCID].getKiller();

			if ( PlayerHandler.players[Player] == null )
			{

				ResetAttackPlayer( NPCID );
				return false;


			}
			else if ( PlayerHandler.players[Player].DirectionCount < 2 )
			{

				return false;
			}
			client plr = ( client ) PlayerHandler.players[Player];
			int EnemyX = PlayerHandler.players[Player].absX;
			int EnemyY = PlayerHandler.players[Player].absY;
			npcs[NPCID].enemyX = EnemyX;
			npcs[NPCID].enemyY = EnemyY;

			if ( ( Math.Abs( npcs[NPCID].absX - EnemyX ) > 20 ) || ( Math.Abs( npcs[NPCID].absY - EnemyY ) > 20 ) )
			{

				ResetAttackPlayer( NPCID );
			}
			int EnemyHP = PlayerHandler.players[Player].playerLevel[PlayerHandler.players[Player].playerHitpoints];
			int EnemyMaxHP = getLevelForXP( PlayerHandler.players[Player].playerXP[PlayerHandler.players[Player].playerHitpoints] );

			if ( PlayerHandler.players[Player].attacknpc == NPCID )
			{
				PlayerHandler.players[Player].faceNPC = NPCID; // Xerozcheez: sets npc index for player to view
				PlayerHandler.players[Player].faceNPCupdate = true; // Xerozcheez: updates face npc index so player faces npcs
				PlayerHandler.players[Player].attacknpc = NPCID;
				PlayerHandler.players[Player].IsAttackingNPC = true;
			}
			int hitDiff = 0;
			hitDiff = misc.random( npcs[NPCID].MaxHit );
			client player = ( client ) PlayerHandler.players[Player];

			if ( player != null )
			{

				int def = player.playerBonus[6];
				int rand = misc.random( def );
				if ( NPCID == 1961 )
				{
					combatLevel[NPCID] = 105;
				}
				int rand_npc = misc.random( combatLevel[NPCID] * 5 );
				if ( npcs[NPCID].npcType == 1472 )
				{
					rand_npc = misc.random( 800 );
				}
				if ( npcs[NPCID].npcType == 80 )
				{
					rand_npc = misc.random( 500 );
				}
				if ( npcs[NPCID].npcType == 1913 )
				{
					rand_npc = misc.random( 1500 );
				}
				if ( npcs[NPCID].npcType == 936 )
				{
					rand_npc = misc.random( 100 );
				}
				if ( npcs[NPCID].npcType == 110 )
				{
					rand_npc = misc.random( 20 );
				}
				int blocked = ( int ) ( def / 10 );

				if ( rand_npc > rand )
				{

					hitDiff = misc.random( npcs[NPCID].MaxHit ) - blocked;

					if ( hitDiff < 0 )
					{

						hitDiff = 0;
					}
				}
				else
				{
					hitDiff = 0;
				}
			}

			if ( ( npcs[NPCID].npcType != 3200 ) && ( npcs[NPCID].npcType != 1645 ) )
			{

				FollowPlayerCB( NPCID, Player );
			}

			if ( ( GoodDistance( npcs[NPCID].absX, npcs[NPCID].absY, EnemyX, EnemyY, 1 ) == true ) || ( npcs[NPCID].npcType == 3200 ) || ( npcs[NPCID].npcType == 2745 ) || ( npcs[NPCID].npcType == 425 ) )
			{

				if ( npcs[NPCID].actionTimer == 0 )
				{

					if ( false && ( EnemyHP <= ( int ) ( ( double ) ( ( double ) EnemyMaxHP / 1000.0 ) ) ) )
					{ //doublecheckthis

					}
					else
					{

						if ( PlayerHandler.players[Player].deathStage > 0 )
						{

							ResetAttackPlayer( NPCID );
						}
						else
						{
							if ( npcs[NPCID].npcType == 941 )
							{
								if ( misc.random( 3 ) == 1 )
								{
									if ( plr.playerEquipment[plr.playerShield] == 1540 )
									{
										hitDiff = 0;
										plr.lowGFX( 579, 0 );
										plr.sendMessage( "You are protected from the dragon's fire!" );
									}
									else
									{
										plr.lowGFX( 579, 0 );
										hitDiff = 10 + misc.random( 20 );
										plr.sendMessage( "You are burnt by the fire!" );
									}
								}
								else
								{
									hitDiff = misc.random( 6 );
								}
							}
							else if ( npcs[NPCID].npcType == 55 )
							{
								if ( misc.random( 3 ) == 1 )
								{
									if ( plr.playerEquipment[plr.playerShield] == 1540 )
									{
										hitDiff = 0;
										plr.lowGFX( 579, 0 );
										plr.sendMessage( "You are protected from the dragon's fire!" );
									}
									else
									{
										plr.lowGFX( 579, 0 );
										hitDiff = 10 + misc.random( 20 );
										plr.sendMessage( "You are burnt by the fire!" );
									}
								}
								else
								{
									hitDiff = misc.random( 9 );
								}
							}
							else if ( npcs[NPCID].npcType == 53 )
							{
								if ( misc.random( 3 ) == 1 )
								{
									if ( plr.playerEquipment[plr.playerShield] == 1540 )
									{
										hitDiff = 0;
										plr.lowGFX( 579, 0 );
										plr.sendMessage( "You are protected from the dragon's fire!" );
									}
									else
									{
										plr.lowGFX( 579, 0 );
										hitDiff = 10 + misc.random( 20 );
										plr.sendMessage( "You are burnt by the fire!" );
									}
								}
								else
								{
									hitDiff = misc.random( 12 );
								}
							}
							else if ( npcs[NPCID].npcType == 54 )
							{
								if ( misc.random( 3 ) == 1 )
								{
									if ( plr.playerEquipment[plr.playerShield] == 1540 )
									{
										hitDiff = 0;
										plr.lowGFX( 579, 0 );
										plr.sendMessage( "You are protected from the dragon's fire!" );
									}
									else
									{
										plr.lowGFX( 579, 0 );
										hitDiff = 10 + misc.random( 20 );
										plr.sendMessage( "You are burnt by the fire!" );
									}
								}
								else
								{
									hitDiff = misc.random( 18 );
								}
							}
							else if ( npcs[NPCID].npcType == 50 && misc.random( 3 ) == 1 )
							{
								if ( plr.playerEquipment[plr.playerShield] == 1540 )
								{
									plr.lowGFX( 579, 0 );
									hitDiff = 3 + misc.random( 7 );
									plr.sendMessage( "You are protected from the dragon's fire!" );
								}
								else if ( plr.playerEquipment[plr.playerShield] != 1540 )
								{
									plr.lowGFX( 579, 0 );
									hitDiff = 20 + misc.random( 30 );
									plr.sendMessage( "You are burnt by the fire!" );
								}
							}
							else if ( npcs[NPCID].npcType == 50 && misc.random( 3 ) == 1 )
							{
								if ( plr.playerEquipment[plr.playerShield] == 1540 )
								{
									plr.specGFX( 396 );
									hitDiff = 3 + misc.random( 7 );
									plr.EntangleDelay = 10;
									plr.sendMessage( "You have been frozen!" );
								}
								else if ( plr.playerEquipment[plr.playerShield] != 1540 )
								{
									plr.specGFX( 396 );
									hitDiff = 20 + misc.random( 30 );
									plr.EntangleDelay = 10;
									plr.sendMessage( "You have been frozen!" );
								}


							}
							if ( npcs[NPCID].npcType == 2025 )
							{ // Ahrim
								int Players = npcs[NPCID].StartKilling;
								client p = ( client ) PlayerHandler.players[Players];
								p.animation( 369, p.absY, p.absX );
								hitDiff = 6 + misc.random( 24 );
							}
							else if ( npcs[NPCID].npcType == 100 )
							{
								npcs[NPCID].animNumber = 309;
							}
							else if ( ( npcs[NPCID].npcType == 941 )
								  || ( npcs[NPCID].npcType == 55 )
								  || ( npcs[NPCID].npcType == 53 ) || ( npcs[NPCID].npcType == 54 ) )
							{
								npcs[NPCID].animNumber = 80;
							}
							else if ( npcs[NPCID].npcType == 50 )
							{
								npcs[NPCID].animNumber = 80;
							}
							if ( server.NpcAnimHandler.atk[npcs[NPCID].npcType] != 0 )
							{

								npcs[NPCID].animNumber = server.NpcAnimHandler.atk[npcs[NPCID].npcType];
							}
							else
							{
								npcs[NPCID].animNumber = 0x326;
							}

							npcs[NPCID].animUpdateRequired = true;
							npcs[NPCID].updateRequired = true;

							if ( ( EnemyHP - hitDiff ) < 0 )
							{

								hitDiff = EnemyHP;
								ResetAttackPlayer( NPCID );
							}
							int id = playerEquipment[playerShield];
							client ppl = ( client ) PlayerHandler.players[Player];
							if ( hitDiff >= 0 )
							{
								ppl.startAnimation( GetBlockAnim( id ) );
							}
							PlayerHandler.players[Player].dealDamage( hitDiff );
							PlayerHandler.players[Player].hitDiff = hitDiff;
							PlayerHandler.players[Player].updateRequired = true;
							PlayerHandler.players[Player].hitUpdateRequired = true;
							PlayerHandler.players[Player].appearanceUpdateRequired = true;
							npcs[NPCID].actionTimer = 7;
						}
					}
					return true;
				}
			}
			return false;
		}

		public int[] playerEquipment = new int[14];//added player defending when attacked - killamess
		public int playerShield = 5;//added player defending when attacked - killamess
		public int GetBlockAnim( int id )
		{
			if ( id == 4755 )
			{
				return 2063;
			}
			if ( id == 4151 )
			{
				return 1659;
			}
			if ( id == 10229 )
			{
				return 1659;
			}
			if ( id == 1171 )
			{
				return 403;
			}
			if ( id == 1185 )
			{
				return 403;
			}
			if ( id == 1187 )
			{
				return 403;
			}
			if ( id == 1191 )
			{
				return 403;
			}
			if ( id == 1201 )
			{
				return 403;
			}
			if ( id == 2659 )
			{
				return 403;
			}
			if ( id == 2667 )
			{
				return 403;
			}
			if ( id == 2675 )
			{
				return 403;
			}
			if ( id == 3122 )
			{
				return 403;
			}
			if ( id == 3488 )
			{
				return 403;
			}
			if ( id == 4156 )
			{
				return 403;
			}
			if ( id == 6524 )
			{
				return 403;
			}
			if ( id == 4153 )
			{
				return 1666;
			}
			if ( id == 1419 )
			{
				return 1666;
			}
			else
			{
				return 1834;
			}
		}
		public Boolean AttackPlayerMage( int NPCID )
		{
			int Player = npcs[NPCID].StartKilling;
			client p = ( client ) PlayerHandler.players[Player];
			if ( PlayerHandler.players[Player] == null )
			{
				ResetAttackPlayer( NPCID );
				return false;
			}
			else if ( PlayerHandler.players[Player].DirectionCount < 2 )
			{
				return false;
			}
			int EnemyHP = PlayerHandler.players[Player].playerLevel[PlayerHandler.players[Player].playerHitpoints];

			if ( PlayerHandler.players[Player].playerEquipment[PlayerHandler.players[Player].playerRing] == 2570 )
			{
				// RingOfLife = true;
			}
			int hitDiff = 0;
			// hitDiff = misc.random(npcs[NPCID].MaxHit);
			if ( npcs[NPCID].actionTimer == 0 )
			{
				if ( false )
				{
				}
				else
				{
					if ( ( PlayerHandler.players[Player].currentHealth > 0 ) == true )
					{
						ResetAttackPlayer( NPCID );
					}
					else
					{
						npcs[NPCID].animNumber = 711; // attack
													  // animation
						if ( npcs[NPCID].npcType == 2591 )
						{ // TzHaar-Mej
							p.animation( 78, p.absY, p.absX );
							hitDiff = 8 + misc.random( 40 );

							npcs[NPCID].animNumber = 1979; // mage
														   // attack
						}


						if ( npcs[NPCID].npcType == 1645 )
						{ // Infernal
						  // Mage
							p.animation( 369, p.absY, p.absX );
							hitDiff = 6 + misc.random( 20 );
						}
						npcs[NPCID].animNumber = 1979; // mage attack
						if ( npcs[NPCID].npcType == 2025 )
						{ // Ahrim
							p.animation( 369, p.absY, p.absX );
							hitDiff = 6 + misc.random( 22 );
						}
						npcs[NPCID].animNumber = 1979; // mage attack
						if ( npcs[NPCID].npcType == 1250 )
						{ // Shade
							p.animation( 383, p.absY, p.absX );
							hitDiff = 1 + misc.random( 30 );
						}
						npcs[NPCID].animNumber = 81; // Dragon breath
													 // attack

						if ( npcs[NPCID].npcType == 509 )
						{
							p.stillgfx( 365, p.absY, p.absX );
							hitDiff = 8 + misc.random( 51 );
						}
						if ( npcs[NPCID].npcType == 1241 )
						{
							p.stillgfx( 363, p.absY, p.absX );
							hitDiff = 2 + misc.random( 19 );
						}
						if ( npcs[NPCID].npcType == 1246 )
						{
							p.stillgfx( 368, npcs[NPCID].absY, npcs[NPCID].absX );
							p.stillgfx( 367, p.absY, p.absX );
							hitDiff = 4 + misc.random( 35 );
						}
						npcs[NPCID].animUpdateRequired = true;
						npcs[NPCID].updateRequired = true;
						if ( ( EnemyHP - hitDiff ) < 0 )
						{
							hitDiff = EnemyHP;
						}
						PlayerHandler.players[Player].hitDiff = hitDiff;
						PlayerHandler.players[Player].updateRequired = true;
						PlayerHandler.players[Player].hitUpdateRequired = true;
						PlayerHandler.players[Player].appearanceUpdateRequired = true;
						npcs[NPCID].actionTimer = 7;
					}
				}
				return true;
			}
			return false;
		}

		public int calcRespawn( int npcid )
		{
			return respawnTime[npcid];
		}

		public void FollowPlayer( int NPCID )
		{
			int follow = npcs[NPCID].followPlayer;
			int playerX = PlayerHandler.players[follow].absX;
			int playerY = PlayerHandler.players[follow].absY;
			npcs[NPCID].RandomWalk = false;
			if ( ( PlayerHandler.players[follow] != null )
					&& ( npcs[NPCID].effects[0] == 0 ) )
			{
				if ( playerY < npcs[NPCID].absY )
				{
					npcs[NPCID].moveX = GetMove( npcs[NPCID].absX, playerX );
					npcs[NPCID].moveY = GetMove( npcs[NPCID].absY, playerY + 1 );
				}
				else if ( playerY > npcs[NPCID].absY )
				{
					npcs[NPCID].moveX = GetMove( npcs[NPCID].absX, playerX );
					npcs[NPCID].moveY = GetMove( npcs[NPCID].absY, playerY - 1 );
				}
				else if ( playerX < npcs[NPCID].absX )
				{
					npcs[NPCID].moveX = GetMove( npcs[NPCID].absX, playerX + 1 );
					npcs[NPCID].moveY = GetMove( npcs[NPCID].absY, playerY );
				}
				else if ( playerX > npcs[NPCID].absX )
				{
					npcs[NPCID].moveX = GetMove( npcs[NPCID].absX, playerX - 1 );
					npcs[NPCID].moveY = GetMove( npcs[NPCID].absY, playerY );
				}
				npcs[NPCID].getNextNPCMovement();
				npcs[NPCID].updateRequired = true;
			}
		}

		public void FollowPlayerCB( int NPCID, int playerID )
		{
			int playerX = PlayerHandler.players[playerID].absX;
			int playerY = PlayerHandler.players[playerID].absY;
			npcs[NPCID].RandomWalk = false;
			npcs[NPCID].faceplayer( playerID );
			if ( PlayerHandler.players[playerID] != null )
			{
				if ( playerY < npcs[NPCID].absY )
				{
					npcs[NPCID].moveX = GetMove( npcs[NPCID].absX, playerX );
					npcs[NPCID].moveY = GetMove( npcs[NPCID].absY, playerY + 1 );
				}
				else if ( playerY > npcs[NPCID].absY )
				{
					npcs[NPCID].moveX = GetMove( npcs[NPCID].absX, playerX );
					npcs[NPCID].moveY = GetMove( npcs[NPCID].absY, playerY - 1 );
				}
				else if ( playerX < npcs[NPCID].absX )
				{
					npcs[NPCID].moveX = GetMove( npcs[NPCID].absX, playerX + 1 );
					npcs[NPCID].moveY = GetMove( npcs[NPCID].absY, playerY );
				}
				else if ( playerX > npcs[NPCID].absX )
				{
					npcs[NPCID].moveX = GetMove( npcs[NPCID].absX, playerX - 1 );
					npcs[NPCID].moveY = GetMove( npcs[NPCID].absY, playerY );
				}
				npcs[NPCID].getNextNPCMovement();
				npcs[NPCID].updateRequired = true;
			}
		}

		public int getLevelForXP( int exp )
		{
			int points = 0;
			int output = 0;

			for ( int lvl = 1; lvl <= 135; lvl++ )
			{
				points += ( Int32 ) Math.Floor( ( double ) lvl + 300.0
						* Math.Pow( 2.0, ( double ) lvl / 7.0 ) );
				output = ( int ) Math.Floor( points / 4d );
				if ( output >= exp )
					return lvl;
			}
			return 0;
		}

		public int GetMove( int Place1, int Place2 )
		{ // Thanks to diablo for this!
		  // Fixed my npc follow code <3
			if ( ( Place1 - Place2 ) == 0 )
				return 0;
			else if ( ( Place1 - Place2 ) < 0 )
				return 1;
			else if ( ( Place1 - Place2 ) > 0 )
				return -1;
			return 0;
		}

		public int GetNPCBlockAnim( int id )
		{
			if ( server.NpcAnimHandler.block[id] != 0 )
			{
				return server.NpcAnimHandler.block[id];
			}
			return 0;
		}

		public int GetNPCDropArrayID( int NPCID, int DropType )
		{
			for ( int i = 0; i < maxNPCDrops; i++ )
			{
				if ( NpcDrops[i] != null )
				{
					if ( ( NpcDrops[i].npcId == NPCID )
							&& ( NpcDrops[i].DropType == DropType ) )
					{
						return i;
					}
				}
			}
			return -1;
		}

		public int GetNpcKiller( int NPCID )
		{
			int Killer = 0;
			int Count = 0;
			for ( int i = 1; i < PlayerHandler.maxPlayers; i++ )
			{
				if ( Killer == 0 )
				{
					Killer = i;
					Count = 1;
				}
				else
				{
					if ( npcs[NPCID].Killing[i] > npcs[NPCID].Killing[Killer] )
					{
						Killer = i;
						Count = 1;
					}
					else if ( npcs[NPCID].Killing[i] == npcs[NPCID].Killing[Killer] )
					{
						Count++;
					}
				}
			}
			if ( ( Count > 1 )
					&& ( npcs[NPCID].Killing[npcs[NPCID].StartKilling] == npcs[NPCID].Killing[Killer] ) )
			{
				Killer = npcs[NPCID].StartKilling;
			}
			return Killer;
		}

		public int GetNpcListHP( int NpcID )
		{
			for ( int i = 0; i < maxListedNPCs; i++ )
			{
				if ( NpcList[i] != null )
				{
					if ( NpcList[i].npcId == NpcID )
					{
						return NpcList[i].npcHealth;
					}
				}
			}
			return 0;
		}

		public void gfxAll( int id, int Y, int X )
		{
			// for (Player p : PlayerHandler.players) {
			foreach ( Player p in PlayerHandler.players )
			{
				if ( p != null )
				{
					client person = ( client ) p;
					if ( person.playerName != null )
					{
						if ( person.distanceToPoint( X, Y ) <= 60 )
						{
							person.stillgfx2( id, Y, X, 0, 0 );
						}
					}
				}
			}
		}

		public Boolean GoodDistance( int objectX, int objectY, int playerX,
				int playerY, int distance )
		{
			for ( int i = 0; i <= distance; i++ )
			{
				for ( int j = 0; j <= distance; j++ )
				{
					if ( ( ( objectX + i ) == playerX )
							&& ( ( ( objectY + j ) == playerY )
									|| ( ( objectY - j ) == playerY ) || ( objectY == playerY ) ) )
					{
						return true;
					}
					else if ( ( ( objectX - i ) == playerX )
						  && ( ( ( objectY + j ) == playerY )
								  || ( ( objectY - j ) == playerY ) || ( objectY == playerY ) ) )
					{
						return true;
					}
					else if ( ( objectX == playerX )
						  && ( ( ( objectY + j ) == playerY )
								  || ( ( objectY - j ) == playerY ) || ( objectY == playerY ) ) )
					{
						return true;
					}
				}
			}
			return false;
		}

		public Boolean IsInRange( int NPCID, int MoveX, int MoveY )
		{
			int NewMoveX = ( npcs[NPCID].absX + MoveX );
			int NewMoveY = ( npcs[NPCID].absY + MoveY );
			if ( ( NewMoveX <= npcs[NPCID].moverangeX1 )
					&& ( NewMoveX >= npcs[NPCID].moverangeX2 )
					&& ( NewMoveY <= npcs[NPCID].moverangeY1 )
					&& ( NewMoveY >= npcs[NPCID].moverangeY2 ) )
			{
				if ( ( ( npcs[NPCID].walkingType == 1 ) && ( IsInWorldMap( NewMoveX,
						NewMoveY ) == true ) )
						|| ( ( npcs[NPCID].walkingType == 2 ) && ( IsInWorldMap2(
								NewMoveX, NewMoveY ) == false ) ) )
				{
					if ( MoveX == MoveY )
					{
						if ( ( ( IsInWorldMap( NewMoveX, npcs[NPCID].absY ) == true ) && ( IsInWorldMap(
								npcs[NPCID].absX, NewMoveY ) == true ) )
								|| ( ( IsInWorldMap2( NewMoveX, npcs[NPCID].absY ) == false ) && ( IsInWorldMap2(
										npcs[NPCID].absX, NewMoveY ) == false ) ) )
						{
							return true;
						}
						return false;
					}
					return true;
				}
			}
			return false;
		}

		public Boolean IsInWorldMap( int coordX, int coordY )
		{
			Boolean a = true;
			if ( a )
				return a;
			for ( int i = 0; i < worldmap[0].Length; i++ )
			{
				// if (worldmap[0][i] == coordX && worldmap[1][i] == coordY) {
				return true;
				// }
			}
			return false;
		}

		public Boolean IsInWorldMap2( int coordX, int coordY )
		{
			Boolean a = true;
			if ( a )
				return a;
			for ( int i = 0; i < worldmap2[0].Length; i++ )
			{
				if ( ( worldmap2[0][i] == coordX ) && ( worldmap2[1][i] == coordY ) )
				{
					return true;
				}
			}
			return false;
		}

		public Boolean loadAutoSpawn( String FileName )
		{
			server.NpcAnimHandler.loadanim();
			String line = "";
			String token = "";
			String token2 = "";
			String token2_2 = "";
			String[] token3 = new String[10];
			Boolean EndOfFile = false;
			TextReader characterfile = null;
			try
			{
				characterfile = File.OpenText( "config\\" + FileName );
			}
			catch ( Exception fileex )
			{
				misc.println( FileName + ": file not found." );
				return false;
			}
			try
			{
				line = characterfile.ReadLine();
			}
			catch ( Exception Exception )
			{
				misc.println( FileName + ": error loading file." );
				return false;
			}
			while ( ( EndOfFile == false ) && ( line != null ) )
			{
				line = line.Trim();
				int spot = line.IndexOf( "=" );
				if ( spot > -1 )
				{
					token = line.Substring( 0, spot );
					token = token.Trim();
					token2 = line.Substring( spot + 1 );
					token2 = token2.Trim();
					token2_2 = token2.Replace( "\t\t", "\t" );
					token2_2 = token2_2.Replace( "\t\t", "\t" );
					token2_2 = token2_2.Replace( "\t\t", "\t" );
					token2_2 = token2_2.Replace( "\t\t", "\t" );
					token2_2 = token2_2.Replace( "\t\t", "\t" );
					token3 = token2_2.Split( "\t" );
					if ( token.Equals( "spawn" ) )
					{
						newNPC( Int32.Parse( token3[0] ), Int32.Parse( token3[1] ), Int32.Parse( token3[2] ),
								Int32.Parse( token3[3] ), Int32.Parse( token3[4] ), Int32.Parse( token3[5] ), 
								Int32.Parse( token3[6] ), Int32.Parse( token3[7] ), Int32.TryParse( token3[8], out var parsed ) ? parsed : 0,
								GetNpcListHP( Int32.Parse( token3[0] ) ) );
					}
				}
				else
				{
					if ( line.Equals( "[ENDOFSPAWNLIST]" ) )
					{
						try
						{
							characterfile.Dispose();
						}
						catch ( Exception Exception )
						{
						}
						return true;
					}
				}
				try
				{
					line = characterfile.ReadLine();
				}
				catch ( Exception Exception1 )
				{
					EndOfFile = true;
				}
			}
			try
			{
				characterfile.Dispose();
			}
			catch ( Exception Exception )
			{
			}
			return false;
		}

		public Boolean loadNPCDrops( String FileName )
		{
			String line = "";
			String token = "";
			String token2 = "";
			String token2_2 = "";
			String[] token3 = new String[10];
			Boolean EndOfFile = false;
			TextReader characterfile = null;
			try
			{
				characterfile = File.OpenText( "config\\"
						+ FileName );
			}
			catch ( Exception fileex )
			{
				misc.println( FileName + ": file not found." );
				return false;
			}
			try
			{
				line = characterfile.ReadLine();
			}
			catch ( Exception Exception )
			{
				misc.println( FileName + ": error loading file." );
				return false;
			}
			while ( ( EndOfFile == false ) && ( line != null ) )
			{
				line = line.Trim();
				int spot = line.IndexOf( "=" );
				if ( spot > -1 )
				{
					token = line.Substring( 0, spot );
					token = token.Trim();
					token2 = line.Substring( spot + 1 );
					token2 = token2.Trim();
					token2_2 = token2.Replace( "\t\t", "\t" );
					token2_2 = token2_2.Replace( "\t\t", "\t" );
					token2_2 = token2_2.Replace( "\t\t", "\t" );
					token2_2 = token2_2.Replace( "\t\t", "\t" );
					token2_2 = token2_2.Replace( "\t\t", "\t" );
					token3 = token2_2.Split( "\t" );
					if ( token.Equals( "npcdrop" ) )
					{
						if ( Int32.Parse( token3[0] ) <= -1 )
							continue;
						drops[Int32.Parse( token3[0] )][dropCount[Int32.Parse( token3[0] )]] = Int32.Parse( token3[1] );
						drops[Int32.Parse( token3[0] )][dropCount[Int32.Parse( token3[0] )] + 1] = Int32.Parse( token3[2] );
						drops[Int32.Parse( token3[0] )][dropCount[Int32.Parse( token3[0] )] + 2] = Int32.Parse( token3[3] );
						dropCount[Int32.Parse( token3[0] )] += 3;
					}
				}
				else
				{
					if ( line.Equals( "[ENDOFNPCDROPLIST]" ) )
					{
						try
						{
							characterfile.Dispose();
						}
						catch ( Exception Exception )
						{
						}
						return true;
					}
				}
				try
				{
					line = characterfile.ReadLine();
				}
				catch ( Exception Exception1 )
				{
					EndOfFile = true;
				}
			}
			try
			{
				characterfile.Dispose();
			}
			catch ( Exception Exception )
			{
			}
			return false;
		}

		public Boolean loadNPCList( String FileName )
		{
			String line = "";
			String token = "";
			String token2 = "";
			String token2_2 = "";
			String[] token3 = new String[10];
			Boolean EndOfFile = false;
			TextReader characterfile = null;
			try
			{
				characterfile = File.OpenText( "config\\"
						+ FileName );
			}
			catch ( Exception fileex )
			{
				misc.println( FileName + ": file not found." );
				return false;
			}
			try
			{
				line = characterfile.ReadLine();
			}
			catch ( Exception Exception )
			{
				misc.println( FileName + ": error loading file." );
				return false;
			}
			while ( ( EndOfFile == false ) && ( line != null ) )
			{
				line = line.Trim();
				int spot = line.IndexOf( "=" );
				if ( spot > -1 )
				{
					token = line.Substring( 0, spot );
					token = token.Trim();
					token2 = line.Substring( spot + 1 );
					token2 = token2.Trim();
					token2_2 = token2.Replace( "\t\t", "\t" );
					token2_2 = token2_2.Replace( "\t\t", "\t" );
					token2_2 = token2_2.Replace( "\t\t", "\t" );
					token2_2 = token2_2.Replace( "\t\t", "\t" );
					token2_2 = token2_2.Replace( "\t\t", "\t" );
					token3 = token2_2.Split( "\t" );
					if ( token.Equals( "npc" ) )
					{
						newNPCList( Int32.Parse( token3[0] ), token3[1], Int32.Parse( token3[2] ), Int32.Parse( token3[3] ) );
						combatLevel[Int32.Parse( token3[0] )] = Int32.Parse( token3[2] );

						if ( token3.Length == 4 ) // no respawn time set.
							respawnTime[Int32.Parse( token3[0] )] = 0;
						else
							respawnTime[Int32.Parse( token3[0] )] = Int32.Parse( token3[4] );
					}
				}
				else
				{
					if ( line.Equals( "[ENDOFNPCLIST]" ) )
					{
						try
						{
							characterfile.Dispose();
						}
						catch ( Exception Exception )
						{
						}
						return true;
					}
				}
				try
				{
					line = characterfile.ReadLine();
				}
				catch ( Exception Exception1 )
				{
					EndOfFile = true;
				}
			}
			try
			{
				characterfile.Dispose();
			}
			catch ( Exception Exception )
			{
			}
			return false;
		}

		public void MonsterDropItem( int newItemID, int newItemAmount, int NPCID )
		{
			int playerid = GetNpcKiller( NPCID );
			Console.WriteLine( "Killer id = " + playerid );
			client player = ( client ) PlayerHandler.players[playerid];
			if ( player != null )
			{
				player.createItem( newItemID, newItemAmount );
			}
			else
			{
				Console.WriteLine( "NULL PLAYER!" );
			}
		}

		public void MonsterDropItems( int NPCID, NPC npc )
		{
			try
			{
				int totalDrops = dropCount[NPCID] / 3;
				if ( totalDrops > 0 )
				{
					// Random roller = new Random();
					for ( int i = 0; i < dropCount[NPCID]; i += 3 )
					{
						double roll = MathHelper.Random() * 100;
						client p = ( client ) PlayerHandler.players[npc
								.getKiller()];
						if ( p != null )
						{
							if ( p.debug )
								p.sendMessage( "Roll:  " + roll + ", Itemid:  "
										+ drops[NPCID][i] + ", amt:  "
										+ drops[NPCID][i + 1] + ", percent:  "
										+ drops[NPCID][i + 2] );
						}
						if ( roll <= drops[NPCID][i + 2] )
						{
							if ( p != null )
							{
								if ( p.debug )
									p.sendMessage( "Rewarding " + drops[NPCID][i] );
							}
							if ( ( drops[NPCID] != null ) && ( npc != null ) )
								ItemHandler.addItem( ( int ) drops[NPCID][i],
										npc.absX, npc.absY,
										( int ) drops[NPCID][i + 1], npc.getKiller(),
										false );
							else
								Console.WriteLine( "ERROR:  NULL DROPS OR NPC" );
						}
					}
				}
			}
			catch ( Exception e )
			{
				//e.printStackTrace();
			}

		}

		public void newNPC( int npcType, int x, int y, int heightLevel, int rangex1,
				int rangey1, int rangex2, int rangey2, int WalkingType, int HP )
		{
			// first, search for a free slot
			int slot = -1;
			for ( int i = 1; i < maxNPCSpawns; i++ )
			{
				if ( npcs[i] == null )
				{
					slot = i;
					break;
				}
			}

			if ( slot == -1 )
				return; // no free slot found
			if ( HP <= 0 )
			{ // This will cause client crashes if we don't
			  // use this :) - xero
				HP = 0;
			}
			NPC newNPC = new NPC( slot, npcType );
			newNPC.absX = x;
			newNPC.absY = y;
			newNPC.makeX = x;
			newNPC.makeY = y;
			newNPC.moverangeX1 = rangex1;
			newNPC.moverangeY1 = rangey1;
			newNPC.moverangeX2 = rangex2;
			newNPC.moverangeY2 = rangey2;
			newNPC.walkingType = WalkingType;
			newNPC.HP = HP;
			newNPC.MaxHP = HP;
			// newNPC.MaxHit = (int)Math.floor((HP / 10));
			newNPC.MaxHit = ( int ) ( combatLevel[npcType] / 3 );
			if ( WalkingType == 1 )
				newNPC.RandomWalk = true;
			if ( newNPC.MaxHit < 1 )
			{
				newNPC.MaxHit = 1;
			}
			newNPC.heightLevel = heightLevel;
			npcs[slot] = newNPC;
		}

		public void newNPCDrop( int npcType, int dropType, int[] Items, int[] ItemsN )
		{
			// first, search for a free slot
			int slot = -1;
			for ( int i = 0; i < maxNPCDrops; i++ )
			{
				if ( NpcDrops[i] == null )
				{
					slot = i;
					break;
				}
			}

			if ( slot == -1 )
				return; // no free slot found

			NPCDrops newNPCDrop = new NPCDrops( npcType );
			newNPCDrop.DropType = dropType;
			newNPCDrop.Items = Items;
			newNPCDrop.ItemsN = ItemsN;
			NpcDrops[slot] = newNPCDrop;
		}

		/*
		 * public Boolean IsInWorldMap(int coordX, int coordY) { for (int i = 0;
		 * i < worldmap[0].Length; i++) { //if (worldmap[0][i] == coordX &&
		 * worldmap[1][i] == coordY) { return true; //} } return false; } public
		 * Boolean IsInWorldMap2(int coordX, int coordY) { for (int i = 0; i <
		 * worldmap2[0].Length; i++) { if (worldmap2[0][i] == coordX &&
		 * worldmap2[1][i] == coordY) { return true; } } return true; }
		 * 
		 * public Boolean IsInRange(int NPCID, int MoveX, int MoveY) { int
		 * NewMoveX = (npcs[NPCID].absX + MoveX); int NewMoveY =
		 * (npcs[NPCID].absY + MoveY); if (NewMoveX <= npcs[NPCID].moverangeX1 &&
		 * NewMoveX >= npcs[NPCID].moverangeX2 && NewMoveY <=
		 * npcs[NPCID].moverangeY1 && NewMoveY >= npcs[NPCID].moverangeY2) { if
		 * ((npcs[NPCID].walkingType == 1 && IsInWorldMap(NewMoveX, NewMoveY) ==
		 * true) || (npcs[NPCID].walkingType == 2 && IsInWorldMap2(NewMoveX,
		 * NewMoveY) == false)) { if (MoveX == MoveY) { if
		 * ((IsInWorldMap(NewMoveX, npcs[NPCID].absY) == true &&
		 * IsInWorldMap(npcs[NPCID].absX, NewMoveY) == true) ||
		 * (IsInWorldMap2(NewMoveX, npcs[NPCID].absY) == false &&
		 * IsInWorldMap2(npcs[NPCID].absX, NewMoveY) == false)) { return true; }
		 * return false; } return true; } } return false; }
		 */

		public void newNPCList( int npcType, String npcName, int combat, int HP )
		{
			// first, search for a free slot
			int slot = -1;
			for ( int i = 0; i < maxListedNPCs; i++ )
			{
				if ( NpcList[i] == null )
				{
					slot = i;
					break;
				}
			}

			if ( slot == -1 )
				return; // no free slot found

			NPCList newNPCList = new NPCList( npcType );
			newNPCList.npcName = npcName;
			newNPCList.npcCombat = combat;
			newNPCList.npcHealth = HP;
			NpcList[slot] = newNPCList;
		}

		public void newSummonedNPC( int npcType, int x, int y, int heightLevel,
				int rangex1, int rangey1, int rangex2, int rangey2,
				int WalkingType, int HP, Boolean Respawns, int summonedBy )
		{
			// first, search for a free slot
			int slot = -1;
			for ( int i = 1; i < maxNPCSpawns; i++ )
			{
				if ( npcs[i] == null )
				{
					slot = i;
					break;
				}
			}

			if ( slot == -1 )
				return; // no free slot found
			if ( HP <= 0 )
			{ // This will cause client crashes if we don't
			  // use this :) - xero
				HP = 100;
			}
			NPC newNPC = new NPC( slot, npcType );
			newNPC.absX = x;
			newNPC.absY = y;
			newNPC.makeX = x;
			newNPC.makeY = y;
			newNPC.moverangeX1 = rangex1;
			newNPC.moverangeY1 = rangey1;
			newNPC.moverangeX2 = rangex2;
			newNPC.moverangeY2 = rangey2;
			newNPC.walkingType = WalkingType;
			newNPC.HP = HP;
			newNPC.MaxHP = HP;
			newNPC.MaxHit = ( int ) Math.Floor( ( HP / 10d ) );
			if ( newNPC.MaxHit < 1 )
			{
				newNPC.MaxHit = 1;
			}
			newNPC.heightLevel = heightLevel;
			newNPC.Respawns = Respawns;
			newNPC.followPlayer = summonedBy;
			newNPC.followingPlayer = true;
			npcs[slot] = newNPC;
		}

		public void poisonNpc( int index )
		{
			npcs[index].poisonDmg = true;
			npcs[index].poisonDelay = misc.random( 60 );
			npcs[index].hitDiff = misc.random( 5 );
			npcs[index].hitDiff = npcs[index].hitDiff;
			npcs[index].updateRequired = true;
			npcs[index].hitUpdateRequired = true;
		}

		public void println( String str )
		{
			Console.WriteLine( str );
		}

		public void process( )
		{
			for ( int i = 0; i < maxNPCSpawns; i++ )
			{
				if ( npcs[i] == null )
					continue;
				npcs[i].clearUpdateFlags();
			}
			for ( int i = 0; i < maxNPCSpawns; i++ )
			{
				if ( npcs[i] != null )
				{
					if ( MagicHandler.itFreezes == true )
					{
						npcs[i].freezeTimer = +10;
						MagicHandler.itFreezes = false;
					}
					if ( npcs[i].freezeTimer > 0 )
					{
						npcs[i].freezeTimer = -1;
					}
					if ( npcs[i].reducedAttack > 0 )
					{
						npcs[i].reducedAttack--;
					}
					if ( npcs[i].reducedAttack == 0 )
					{
						npcs[i].MaxHit = ( int ) Math.Floor( ( npcs[i].MaxHP / 10d ) );
						npcs[i].reducedAttack = -1;
					}
					if ( npcs[i].poisonTimer > 0 )
					{
						npcs[i].poisonTimer--;
					}
					if ( npcs[i].poisonTimer == 0 )
					{
						npcs[i].poisonDmg = false;
						npcs[i].poisonDelay = -1;
						npcs[i].poisonTimer = -1;
					}
					if ( npcs[i].poisonDelay > 0 )
					{
						npcs[i].poisonDelay--;
					}
					if ( ( npcs[i].poisonTimer > 0 ) && ( npcs[i].poisonDelay == 0 ) )
					{
						poisonNpc( i );
					}
					if ( npcs[i].actionTimer > 0 )
					{
						npcs[i].actionTimer--;
					}

					for ( int i1 = 0; i1 < npcs[i].effects.Length; i1++ )
					{
						if ( npcs[i].effects[i1] > 0 )
						{
							npcs[i].effects[i1]--;
						}
					}

					if ( npcs[i].IsDead == false )
					{
						if ( ( npcs[i].npcType == 1268 ) || ( npcs[i].npcType == 1266 ) )
						{
							for ( int j = 1; j < PlayerHandler.maxPlayers; j++ )
							{
								if ( PlayerHandler.players[j] != null )
								{
									if ( ( GoodDistance( npcs[i].absX, npcs[i].absY,
											PlayerHandler.players[j].absX,
											PlayerHandler.players[j].absY, 2 ) == true )
											&& ( npcs[i].IsClose == false ) )
									{
										npcs[i].actionTimer = 10;
										npcs[i].IsClose = true;
									}
								}
							}
							if ( ( npcs[i].actionTimer == 0 )
									&& ( npcs[i].IsClose == true ) )
							{
								for ( int j = 1; j < PlayerHandler.maxPlayers; j++ )
								{
									if ( PlayerHandler.players[j] != null )
									{
										PlayerHandler.players[j].RebuildNPCList = true;
									}
								}
								if ( npcs[i].Respawns )
								{
									int old1 = ( npcs[i].npcType - 1 );
									int old2 = npcs[i].makeX;
									int old3 = npcs[i].makeY;
									int old4 = npcs[i].heightLevel;
									int old5 = npcs[i].moverangeX1;
									int old6 = npcs[i].moverangeY1;
									int old7 = npcs[i].moverangeX2;
									int old8 = npcs[i].moverangeY2;
									int old9 = npcs[i].walkingType;
									int old10 = npcs[i].MaxHP;
									npcs[i] = null;
									newNPC( old1, old2, old3, old4, old5, old6,
											old7, old8, old9, old10 );
								}
							}
						}
						else if ( ( npcs[i].RandomWalk == true )
							  && ( misc.random2( 10 ) == 1 )
							  && ( npcs[i].moverangeX1 > 0 )
							  && ( npcs[i].moverangeY1 > 0 )
							  && ( npcs[i].moverangeX2 > 0 )
							  && ( npcs[i].moverangeY2 > 0 ) )
						{ // Move
						  // NPC
							int MoveX = misc.random( 1 );
							int MoveY = misc.random( 1 );
							int Rnd = misc.random2( 4 );
							if ( Rnd == 1 )
							{
								MoveX = -( MoveX );
								MoveY = -( MoveY );
							}
							else if ( Rnd == 2 )
							{
								MoveX = -( MoveX );
							}
							else if ( Rnd == 3 )
							{
								MoveY = -( MoveY );
							}
							if ( IsInRange( i, MoveX, MoveY ) == true )
							{
								npcs[i].moveX = MoveX;
								npcs[i].moveY = MoveY;
							}
							npcs[i].updateRequired = true;
						}
						if ( ( npcs[i].RandomWalk == false )
								&& ( npcs[i].IsUnderAttack == true )
								&& ( npcs[i].effects[0] == 0 ) )
						{
							if ( ( npcs[i].npcType == 1645 )
									|| ( npcs[i].npcType == 509 )
									|| ( npcs[i].npcType == 1241 )
									|| ( npcs[i].npcType == 1246 )
									|| ( npcs[i].npcType == 2591 )
									|| ( npcs[i].npcType == 2025 )
									|| ( npcs[i].npcType == 1250 ) )
								AttackPlayerMage( i );
							else
								AttackPlayer( i );
						}
						else if ( npcs[i].followingPlayer
							  && ( npcs[i].followPlayer > 0 )
							  && ( PlayerHandler.players[npcs[i].followPlayer] != null )
							  && ( PlayerHandler.players[npcs[i].followPlayer].currentHealth > 0 )
							  && ( npcs[i].effects[0] == 0 ) )
						{
							if ( PlayerHandler.players[npcs[i].followPlayer].AttackingOn > 0 )
							{
								int follow = npcs[i].followPlayer;
								npcs[i].StartKilling = PlayerHandler.players[follow].AttackingOn;
								npcs[i].RandomWalk = true;
								npcs[i].IsUnderAttack = true;
								if ( npcs[i].StartKilling > 0 )
								{
									if ( ( npcs[i].npcType == 1645 )
											|| ( npcs[i].npcType == 509 )
											|| ( npcs[i].npcType == 1241 )
											|| ( npcs[i].npcType == 1246 )
											|| ( npcs[i].npcType == 2591 )
											|| ( npcs[i].npcType == 2025 )
											|| ( npcs[i].npcType == 1250 ) )
										AttackPlayerMage( i );
									else
										AttackPlayer( i );
								}

							}
							else
							{
								FollowPlayer( i );
							}
						}
						else if ( npcs[i].followingPlayer
							  && ( npcs[i].followPlayer > 0 )
							  && ( PlayerHandler.players[npcs[i].followPlayer] != null )
							  && ( PlayerHandler.players[npcs[i].followPlayer].currentHealth > 0 )
							  && ( npcs[i].effects[0] == 0 ) )
						{
							if ( PlayerHandler.players[npcs[i].followPlayer].attacknpc > 0 )
							{
								int follow = npcs[i].followPlayer;
								npcs[i].attacknpc = PlayerHandler.players[follow].attacknpc;
								npcs[i].IsUnderAttackNpc = true;
								npcs[npcs[i].attacknpc].IsUnderAttackNpc = true;
								if ( npcs[i].attacknpc > 0 )
								{
									if ( ( npcs[i].npcType == 1645 )
											|| ( npcs[i].npcType == 509 )
											|| ( npcs[i].npcType == 1241 )
											|| ( npcs[i].npcType == 1246 )
											|| ( npcs[i].npcType == 2591 )
											|| ( npcs[i].npcType == 2025 )
											|| ( npcs[i].npcType == 1250 ) )
										AttackNPCMage( i );
									else
										AttackNPC( i );
								}
							}
							else
							{
								FollowPlayer( i );
							}
						}
						else if ( npcs[i].IsUnderAttackNpc == true )
						{
							AttackNPC( i );
							if ( misc.random( 50 ) == 1 )
							{
								npcs[i].updateRequired = true;
								npcs[i].textUpdateRequired = true;
								npcs[i].textUpdate = "I had ya ma last night bitch";
							}
							if ( misc.random( 50 ) == 3 )
							{
								npcs[i].updateRequired = true;
								npcs[i].textUpdateRequired = true;
								npcs[i].textUpdate = "Haha I own you neeb";
							}
							if ( misc.random( 50 ) == 5 )
							{
								npcs[i].updateRequired = true;
								npcs[i].textUpdateRequired = true;
								npcs[i].textUpdate = "Cmon then bitch";
							}
							if ( misc.random( 50 ) == 7 )
							{
								npcs[i].updateRequired = true;
								npcs[i].textUpdateRequired = true;
								npcs[i].textUpdate = "ARGHH!";
							}
							if ( misc.random( 50 ) == 9 )
							{
								npcs[i].updateRequired = true;
								npcs[i].textUpdateRequired = true;
								npcs[i].textUpdate = "Owch that hurt!";
							}
						}/*
					 * } else if (i == 94) { npcs[i].attacknpc = 95;
					 * npcs[i].IsUnderAttackNpc = true;
					 * npcs[95].IsUnderAttackNpc = true; AttackNPC(i); }
					 * else if (i == 96) { npcs[i].attacknpc = 97;
					 * npcs[i].IsUnderAttackNpc = true;
					 * npcs[97].IsUnderAttackNpc = true; AttackNPC(i); }
					 */
						if ( npcs[i].RandomWalk == true )
						{
							npcs[i].getNextNPCMovement();
						}
						if ( npcs[i].npcType == 1282 )
						{
							if ( misc.random( 15 ) == 1 )
							{
								npcs[i].updateRequired = true;
								npcs[i].textUpdateRequired = true;
								npcs[i].textUpdate = "Buying all market goods for a fair price!";
							}
							else if ( misc.random( 15 ) == 7 )
							{
								npcs[i].updateRequired = true;
								npcs[i].textUpdateRequired = true;
								npcs[i].textUpdate = "This is your lucky day, i got the best offers!";
							}
						}
						if ( ( npcs[i].npcType == 81 ) || ( npcs[i].npcType == 397 )
								|| ( npcs[i].npcType == 1766 )
								|| ( npcs[i].npcType == 1767 )
								|| ( npcs[i].npcType == 1768 ) )
						{
							if ( misc.random2( 50 ) == 1 )
							{
								npcs[i].updateRequired = true;
								npcs[i].textUpdateRequired = true;
								npcs[i].textUpdate = "Moo";
							}
						}
					}
					else if ( npcs[i].IsDead == true )
					{
						if ( ( npcs[i].actionTimer == 0 )
								&& ( npcs[i].DeadApply == false )
								&& ( npcs[i].NeedRespawn == false ) )
						{
							if ( ( npcs[i].npcType == 81 ) || ( npcs[i].npcType == 397 )
									|| ( npcs[i].npcType == 1766 )
									|| ( npcs[i].npcType == 1767 )
									|| ( npcs[i].npcType == 1768 ) )
							{
								npcs[i].animNumber = 0x03E; // cow dead
							}
							else if ( npcs[i].npcType == 41 )
							{
								npcs[i].animNumber = 0x039; // chicken dead
							}
							else if ( npcs[i].npcType == 87 )
							{
								npcs[i].animNumber = 0x08D; // rat dead
							}

							else if ( npcs[i].npcType == 3200 )
							{
								npcs[i].animNumber = 3147; // drags: chaos ele
														   // emote ( YESSS )
							}
							else if ( npcs[i].npcType == 1605 )
							{
								npcs[i].animNumber = 1508; // drags: abberant
														   // spector ( YAY )
							}
							else if ( npcs[i].npcType == 1961 )
							{
								npcs[i].animNumber = 1929; // drags: abberant
														   // spector ( YAY )
							}
							else if ( ( npcs[i].npcType == 221 )
								  || ( npcs[i].npcType == 110 ) )
							{
								npcs[i].animNumber = 131; // drags: abberant
														  // spector ( YAY )
							}
							else if ( ( npcs[i].npcType == 941 )
								  || ( npcs[i].npcType == 55 )
								  || ( npcs[i].npcType == 53 ) )
							{
								npcs[i].animNumber = 92;
							}
							else if ( npcs[i].npcType == 114 )
							{
								npcs[i].animNumber = 361;
							}
							else if ( npcs[i].npcType == 86 )
							{
								npcs[i].animNumber = 141;
							}
							else if ( npcs[i].npcType == 1125 )
							{
								npcs[i].animNumber = 1476;
							}
							else if ( npcs[i].npcType == 142 )
							{
								npcs[i].animNumber = 78;
							}
							else if ( npcs[i].npcType == 49 )
							{
								println( "setting emote.." );
								npcs[i].animNumber = 161;
							}
							else
							{
								npcs[i].animNumber = 0x900; // human dead
							}

							npcs[i].updateRequired = true;
							npcs[i].animUpdateRequired = true;
							npcs[i].DeadApply = true;
							npcs[i].actionTimer = 10;

							if ( npcs[i].followingPlayer
									&& ( PlayerHandler.players[npcs[i].followPlayer] != null ) )
								PlayerHandler.players[npcs[i].followPlayer].summonedNPCS--;
						}
						else if ( ( npcs[i].actionTimer == 0 )
							  && ( npcs[i].DeadApply == true )
							  && ( npcs[i].NeedRespawn == false )
							  && ( npcs[i] != null ) )
						{
							// Console.WriteLine("Killer=" + npcs[i].StartKilling);
							client temp = ( client ) PlayerHandler.players[npcs[i]
									.getKiller()];
							if ( ( temp != null ) && !temp.disconnected )
							{
								if ( npcs[i].npcType == 1125 )
								{
									server.playerHandler
											.yell( "Dad has been slain by "
													+ temp.playerName + " (level-"
													+ temp.combatLevel + ")" );
								}
								else if ( npcs[i].npcType == 936 )
								{
									server.playerHandler
											.yell( "San Tojalon has been slain by "
													+ temp.playerName + " (level-"
													+ temp.combatLevel + ")" );
								}
								else if ( npcs[i].npcType == 3 )
								{
									int Player = npcs[i].StartKilling;
									client ppl = ( client ) PlayerHandler.players[Player];
									ppl.teleportToX = 3222;
									ppl.teleportToY = 3218;
									ppl.sendMessage( "" );
									ppl.sendMessage( "" );
									ppl.sendMessage( "" );
									ppl.sendMessage( "" );
									ppl.sendMessage( "You have learned your basic combat training." );
									ppl.sendMessage( "You are ready to fight!" );



								}
								else if ( npcs[i].npcType == 221 )
								{
									server.playerHandler
											.yell( "The Black Knight Titan has been slain by "
													+ temp.playerName
													+ " (level-"
													+ temp.combatLevel + ")" );
								}
								else if ( npcs[i].npcType == 1613 )
								{
									server.playerHandler
											.yell( "Nechryael has been slain by "
													+ temp.playerName + " (level-"
													+ temp.combatLevel + ")" );

								}
								MonsterDropItems( npcs[i].npcType, npcs[i] );
								temp.attackedNpc = false;
								temp.attackedNpcId = -1;
							}
							npcs[i].NeedRespawn = true;
							npcs[i].actionTimer = calcRespawn( npcs[i].npcType );
							npcs[i].absX = npcs[i].makeX;
							npcs[i].absY = npcs[i].makeY;
							npcs[i].animNumber = 0x328;
							npcs[i].HP = npcs[i].MaxHP;
							npcs[i].updateRequired = true;
							npcs[i].animUpdateRequired = true;

						}
						else if ( ( npcs[i].actionTimer == 0 )
							  && ( npcs[i].NeedRespawn == true ) )
						{
							for ( int j = 1; j < PlayerHandler.maxPlayers; j++ )
							{
								if ( PlayerHandler.players[j] != null )
								{
									PlayerHandler.players[j].RebuildNPCList = true;
								}
							}
							int old1 = npcs[i].npcType;
							if ( ( old1 == 1267 ) || ( old1 == 1265 ) )
							{
								old1 += 1;
							}
							int old2 = npcs[i].makeX;
							int old3 = npcs[i].makeY;
							int old4 = npcs[i].heightLevel;
							int old5 = npcs[i].moverangeX1;
							int old6 = npcs[i].moverangeY1;
							int old7 = npcs[i].moverangeX2;
							int old8 = npcs[i].moverangeY2;
							int old9 = npcs[i].walkingType;
							int old10 = npcs[i].MaxHP;
							npcs[i] = null;
							newNPC( old1, old2, old3, old4, old5, old6, old7, old8,
									old9, old10 );

						}
					}
				}
			}
		}

		public Boolean ResetAttackNPC( int NPCID )
		{
			server.npcHandler.npcs[NPCID].IsUnderAttackNpc = false;
			server.npcHandler.npcs[NPCID].IsAttackingNPC = false;
			server.npcHandler.npcs[NPCID].attacknpc = -1;
			server.npcHandler.npcs[NPCID].RandomWalk = true;
			server.npcHandler.npcs[NPCID].animNumber = 0x328;
			server.npcHandler.npcs[NPCID].animUpdateRequired = true;
			server.npcHandler.npcs[NPCID].updateRequired = true;
			return true;
		}

		public Boolean ResetAttackPlayer( int NPCID )
		{
			server.npcHandler.npcs[NPCID].IsUnderAttack = false;
			server.npcHandler.npcs[NPCID].StartKilling = 0;
			server.npcHandler.npcs[NPCID].RandomWalk = true;
			server.npcHandler.npcs[NPCID].animNumber = 0x328;
			server.npcHandler.npcs[NPCID].animUpdateRequired = true;
			server.npcHandler.npcs[NPCID].updateRequired = true;
			return true;
		}
	}

}
