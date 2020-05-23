﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sharp317
{
	public class Item
	{

		public static Int32[] weapons = new Int32[] { 15156, 6818, 6760, 6762, 6764, 6739, 6745, 6746, 6724, 6587, 6589, 6591, 6593, 6595, 6597, 6599, 6601, 6603, 6605, 6607, 6609, 6611, 6613, 6313, 6315, 6317, 6562, 6563, 6525, 6526, 6527, 6528, 6523, 522, 6408, 6410, 6412, 6414, 6416, 6418, 6420, 5628, 5629, 5630, 5631, 5632, 5633, 5634, 5635, 536, 5637, 5638, 5639, 5640, 5641, 5642, 5643, 5644, 5645, 5646, 5647, 5648, 5649, 5650, 5651, 562, 5653, 5654, 5655, 5656, 5657, 5658, 5659, 5660, 5661, 5662, 5663, 5664, 5665, 5666, 5667, 566, 5670, 5672, 5674, 5676, 5678, 5680, 5682, 5684, 5686, 5688, 5690, 5692, 5694, 5696, 5698, 5700, 5702, 5704, 5706, 5708, 5710, 5712, 5714, 5716, 5718, 5720, 5722, 5724, 5726, 5728, 5730, 5734, 736, 5018, 5016, 4982, 4983, 4984, 4985, 4958, 4959, 4960, 4961, 4934, 4935, 4936, 4937, 4910, 411, 4912, 4913, 4862, 4863, 4864, 4865, 4710, 4718, 4726, 4734, 4747, 4755, 4599, 4600, 4675, 457, 4580, 4582, 4584, 4566, 4565, 4508, 4503, 4236, 4212, 4214, 4215, 4216, 4217, 4218, 4219, 422, 4221, 4222, 4223, 4170, 4158, 4151, 4153, 4023, 4024, 4025, 4026, 4027, 4028, 4029, 4030, 4031, 4068, 3899, 3757, 3170, 3171, 3172, 3173, 3174, 3175, 3176, 3190, 3192, 3194, 3196, 3198, 3200, 202, 3204, 3093, 3094, 3095, 3096, 397, 3098, 3099, 3100, 3101, 3054, 3053, 2997, 2963, 2961, 252, 2883, 2460, 2462, 2464, 2466, 2468, 2470, 2472, 2474, 2476, 2415, 2416, 2417, 35, 667, 732, 76, 747, 751, 767, 772, 800, 801, 802, 803, 804, 805, 806, 807, 808, 809, 810, 811, 812, 813, 814, 81, 816, 817, 818, 825, 826, 827, 828, 829, 830, 831, 832, 833, 834, 835, 836, 837, 839, 841, 843, 845, 847, 849, 851, 853, 855, 857, 859, 861, 863, 864, 865, 866, 867, 868, 869, 870, 871, 872, 873, 874, 75, 876, 1203, 1205, 1207, 1209, 1211, 1213, 1215, 1217, 1219, 1221, 1223, 1225, 1227, 1229, 123, 1233, 1235, 1237, 1239, 1241, 1243, 1245, 1247, 1249, 1251, 1253, 1255, 1257, 1259, 1261, 1263, 1265, 1267, 1269, 1271, 1273, 1275, 1277, 1279, 1281, 1283, 1285, 1287, 1289, 1291, 1293, 1295, 297, 1299, 1301, 1303, 1305, 1307, 1309, 1311, 1313, 1315, 1317, 1319, 1321, 1323, 1325, 1327, 129, 1331, 1333, 1335, 1337, 1339, 1341, 1343, 1345, 1347, 1349, 1351, 1353, 1355, 1357, 1359, 131, 1363, 1365, 1367, 1369, 1371, 1373, 1375, 1377, 1379, 1381, 1383, 1385, 1387, 1389, 1391, 139, 1395, 1397, 1399, 1401, 1403, 1405, 1407, 1409, 1419, 1420, 1422, 1424, 426, 1428, 1430, 1432, 1434, 2402 };
		public static Int32[] amulets = new Int32[] { 14510, 1654, 6707, 6544, 5521, 4306, 3853, 3855, 3857, 3859, 3861, 3863, 3865, 3867, 2406, 1664, 1662, 1660, 1658, 1656, 1009, 774, 616, 1796, 6581, 6577, 1716, 1722, 1718, 1724, 6857, 6859, 6861, 683, 7803, 6585, 86, 87, 295, 421, 552, 589, 1478, 1692, 1694, 1696, 1698, 1700, 1702, 1704, 1706, 108, 1710, 1712, 1725, 1727, 1729, 1731, 4021, 4081, 4250, 4677, 6040, 6041, 6208 };
		public static Int32[] arrows = new Int32[] { 6061, 4773, 4778, 4783, 4788, 4793, 4798, 4803, 78, 598, 877, 878, 879, 880, 881, 882, 883, 884, 85, 886, 887, 888, 889, 890, 891, 892, 893, 942, 2532, 2533, 2534, 2535, 2536, 2537, 2538, 2539, 240, 2541, 2866, 4160, 4172, 4173, 4174, 4175, 4740, 5616, 5617, 5618, 5619, 5620, 5621, 5622, 563, 5624, 5625, 5626, 5627, 6061, 6062 };
		public static Int32[] body = new Int32[] { 13591, 14638, 14512, 14507, 14503, 5024, 4988, 4989, 4990, 4991, 4940, 4941, 4942, 4943, 4868, 4869, 4870, 4871, 2405, 1757, 1005, 6786, 6750, 6065, 430, 75, 6371, 6361, 6351, 6341, 581, 3793, 577, 546, 544, 5026, 5028, 6394, 642, 426, 5030, 5032, 5034, 3767, 3769, 3771, 3773, 3775, 6617, 6615, 7134, 7110, 7122, 7128, 11311133, 1135, 1129, 6322, 7362, 7364, 2896, 2906, 2916, 2926, 2936, 1844, 636, 638, 640, 642, 644, 592, 6129, 4298, 1135, 2499, 2501, 6654, 7374, 7376, 7370, 7372, 6139, 2503, 7399, 7390, 7392, 575, 6916, 1035, 540, 5553, 4757, 1833, 6388, 6384, 2501, 2499, 1355, 4111, 4101, 4091, 6186, 618, 6180, 3058, 4509, 4504, 4069, 4728, 4736, 4712, 6107, 2661, 3140, 1101, 1103, 1105, 1107, 11091111, 1113, 1115, 1117, 1119, 1121, 1123, 1125, 1127, 1129, 1131, 1133, 1135, 2499, 2501, 2583, 591, 2599, 2607, 2615, 2623, 2653, 2669, 3387, 3481, 4712, 4720, 4728, 4749, 4892, 4893, 4894, 495, 4916, 4917, 4918, 4919, 4964, 4965, 4966, 4967, 6107, 6133, 6322 };
		public static Int32[] boots = new Int32[] { 15352, 6666, 6790, 6377, 6367, 6357, 3393, 1837, 1846, 3105, 3107, 3700, 3791, 5064, 7159, 88, 6619, 714, 6328, 6920, 6349, 7596, 6106, 88, 89, 626, 628, 630, 632, 634, 1061, 1837, 1846, 2577, 2579, 294, 2904, 2914, 2924, 2934, 3061, 3105, 3107, 3791, 4097, 4107, 4117, 4119, 4121, 4123, 4125, 417, 4129, 4131, 4310, 5064, 5345, 5557, 6069, 6106, 6143, 6145, 6147, 6328 };
		public static Int32[] capes = new Int32[] { 14073, 14074, 14076, 14077, 14079, 14080, 14082, 14083, 14085, 14086, 14088, 14089, 14091, 14092, 14094, 14095, 14097, 14098, 14100, 14101, 14103, 14104, 14106, 14107, 14109, 14110, 14112, 14113, 14115, 14116, 14118, 14119, 14121, 14122, 14124, 14125, 14127, 14128, 14130, 14131, 14133, 14134, 14136, 14137, 14139, 14140, 7918, 7535, 3759, 3761, 3763, 3765, 3777, 3779, 3781, 3783, 3785, 3787, 3789, 4041, 4042, 4514, 4516, 6111, 6570, 6568, 1007, 1019, 1021, 1023, 1027, 1029, 1031, 1052, 2412, 2413, 2414, 4304, 315, 4317, 4319, 4321, 4323, 4325, 4327, 4329, 4331, 4333, 4335, 4337, 4339, 4341, 4343, 4345, 447, 4349, 4351, 4353, 4355, 4357, 4359, 4361, 4363, 4365, 4367, 4369, 4371, 4373, 4375, 4377, 439, 4381, 4383, 4385, 4387, 4389, 4391, 4393, 4395, 4397, 4399, 4401, 4403, 4405, 4407, 4409, 441, 4413, 4514, 4516, 6070, 6568, 6570 };
		public static Int32[] gloves = new Int32[] { 4105, 7453, 7454, 7455, 7456, 7457, 7458, 7459, 7460, 7461, 7462, 6720, 6379, 6369, 6359, 6347, 1495, 1580, 6068, 3391, 6629, 6330, 6922, 7595, 2491, 1065, 2487, 2489, 3060, 1495, 775, 776, 777, 778, 6708, 1059, 1063, 1065, 1580, 2487, 2489, 2491, 2902, 2912, 2922, 2932, 2942, 3060, 3799, 4095, 105, 4115, 4308, 5556, 6068, 6110, 6149, 6151, 6153 };
		public static Int32[] hats = new Int32[] { 14513, 15309, 15311, 15310, 15195, 1042, 14509, 14075, 14078, 14120, 14123, 14126, 14129, 14132, 14135, 14138, 14081, 14084, 14087, 14090, 14093, 14096, 14099, 14102, 14105, 14108, 14111, 14114, 14117, 4166, 7917, 4168, 1025, 1167, 1169, 7003, 4502, 6665, 1506, 6548, 6547, 7319, 7321, 7323, 7325, 7327, 6885, 6886, 6887, 2645, 2647, 2649, 7534, 7539, 3327, 3329, 3331, 3333, 3335, 3337, 3339, 341, 3343, 74, 6621, 6623, 7394, 7396, 7112, 7124, 7130, 7136, 7594, 6856, 6858, 6860, 6862, 632, 7400, 6656, 4856, 4857, 4858, 4859, 4880, 4881, 4882, 4883, 4904, 4905, 4906, 4907, 4928, 49294930, 4931, 4952, 4953, 4954, 4955, 4976, 4977, 4978, 4979, 4732, 4753, 4611, 6188, 6182, 4511, 056, 4071, 4724, 2639, 2641, 2643, 2665, 6109, 5525, 5527, 5529, 5531, 5533, 5535, 5537, 5539, 541, 5543, 5545, 5547, 5549, 5551, 74, 579, 656, 658, 660, 662, 664, 740, 1017, 1037, 1038, 1040, 142, 1044, 1046, 1048, 1050, 1053, 1055, 1057, 1137, 1139, 1141, 1143, 1145, 1147, 1149, 1151, 113, 1155, 1157, 1159, 1161, 1163, 1165, 1949, 2422, 2581, 2587, 2595, 2605, 2613, 2619, 2627, 263, 2633, 2635, 2637, 2651, 2657, 2673, 2900, 2910, 2920, 2930, 2940, 2978, 2979, 2980, 2981, 2982, 2983, 2984, 2985, 2986, 2987, 2988, 2989, 2990, 2991, 2992, 2993, 2994, 2995, 3057, 3385, 3486, 748, 3749, 3751, 3753, 3755, 3797, 4041, 4042, 4071, 4089, 4099, 4109, 4164, 4302, 4506, 4511, 413, 4515, 4551, 4567, 4708, 4716, 4724, 480, 4881, 4882, 483, 4904, 4905, 4906, 4907, 4952, 4953, 4954, 4955, 4976, 4977, 4978, 4979, 5013, 5014, 5554, 557, 6109, 6128, 6131, 6137, 6182, 6188, 6335, 6337, 6339, 6345, 6355, 6365, 6375, 6382, 6392, 6400, 6918 };
		public static Int32[] shields = new Int32[] { 13595, 13596, 13597, 13598, 13599, 13600, 13601, 7676, 6788, 6789, 6889, 2997, 7332, 7334, 7336, 7338, 7340, 7342, 7344, 7346, 7348, 7350, 7352, 7354, 7356, 7358, 7360, 6631, 6633, 7053, 1171, 1173, 1175, 1177, 1179, 1181, 1183, 1185, 1187, 189, 1191, 1193, 1195, 1197, 1199, 1201, 1540, 2589, 2597, 2603, 2611, 2621, 2629, 2659, 2667, 275, 2890, 3122, 3488, 3758, 3839, 3840, 3841, 3842, 3843, 3844, 4072, 4156, 4224, 4225, 4226, 427, 4228, 4229, 4230, 4231, 4232, 4233, 4234, 4507, 4512, 6215, 6217, 6219, 6221, 6223, 6225, 622, 6229, 6231, 6233, 6235, 6237, 6239, 6241, 6243, 6245, 6247, 6249, 6251, 6253, 6255, 6257, 6259, 6261, 6263, 6265, 6267, 6269, 6271, 6273, 6275, 6277, 6279, 6524 };
		public static Int32[] fullHelm = new Int32[] { 15309, 15311, 15310, 15195, 14509, 14075, 14078, 14120, 14123, 14126, 14129, 14132, 14135, 14138, 14081, 14084, 14087, 14090, 14093, 14096, 14099, 14102, 14105, 14108, 14111, 14114, 14117, 4976, 4977, 4978, 4979, 4952, 4953, 4954, 4955, 4928, 4929, 4930, 4931, 4904, 4905, 4906, 4907, 4880, 4881, 4882, 4883, 4856, 4857, 4858, 4859, 4856, 4567, 4515, 4513, 4302, 74, 3748, 6137, 611, 6128, 6621, 1151, 1143, 1145, 1147, 1141, 1137, 1139, 1149, 4753, 4708, 4716, 4745, 7003, 753, 4551, 4745, 6623, 5574, 7112, 7124, 7130, 7136, 7594, 6326, 4732, 4753, 6188, 4511, 4506, 40714724, 6109, 2665, 1153, 1155, 1157, 1159, 1161, 1163, 1165, 2587, 2595, 2605, 2613, 2619, 2627, 657, 2673, 3486, 6402, 6394 };
		public static Int32[] fullMask = new Int32[] { 15309, 15311, 15310, 14513, 14505, 1167, 1169, 1506, 6326, 4732, 4708, 4724, 4716, 4732, 5554, 4611, 6188, 3507, 4511, 4056, 4071, 4724, 2665, 6109, 1053, 1055, 1057 };
		public static Int32[] platebody = new Int32[] { 13591, 14638, 6065, 5028, 5026, 5024, 2405, 6786, 6750, 75, 6371, 6361, 6351, 6341, 3793, 577, 581, 546, 544, 26, 3767, 3769, 3771, 3773, 3775, 6617, 6322, 2896, 2906, 2916, 2926, 2936, 1844, 636, 638, 640, 42, 644, 5575, 6129, 6139, 6133, 4298, 7399, 6916, 7390, 7392, 5032, 5034, 5030, 1035, 540, 55534757, 1833, 1835, 6388, 6384, 1355, 4111, 4101, 4868, 4869, 4870, 4871, 4892, 4893, 4894, 4895, 916, 4917, 4918, 4919, 4940, 4941, 4942, 4943, 4964, 4965, 4966, 4967, 4988, 4989, 4990, 4991, 491, 6186, 6184, 6180, 3058, 4509, 4504, 4069, 4728, 4736, 4712, 6107, 2661, 3140, 1115, 1117, 119, 1121, 1123, 1125, 1127, 2583, 2591, 2599, 2607, 2615, 2623, 2653, 2669, 3481, 4720, 4728, 474, 2661 };
		public static Int32[] shield = new Int32[] { 7676, 6788, 6789, 6889, 2997, 7332, 7334, 7336, 7338, 7340, 7342, 7344, 7346, 7348, 7350, 7352, 7354, 7356, 7358, 7360, 6631, 6633, 7053, 1171, 1173, 1175, 1177, 1179, 1181, 1183, 1185, 1187, 189, 1191, 1193, 1195, 1197, 1199, 1201, 1540, 2589, 2597, 2603, 2611, 2621, 2629, 2659, 2667, 275, 2890, 3122, 3488, 3758, 3839, 3840, 3841, 3842, 3843, 3844, 4072, 4156, 4224, 4225, 4226, 427, 4228, 4229, 4230, 4231, 4232, 4233, 4234, 4507, 4512, 6215, 6217, 6219, 6221, 6223, 6225, 622, 6229, 6231, 6233, 6235, 6237, 6239, 6241, 6243, 6245, 6247, 6249, 6251, 6253, 6255, 6257, 6259, 6261, 6263, 6265, 6267, 6269, 6271, 6273, 6275, 6277, 6279, 6524 };
		public static Int32[] rings = new Int32[] { 6731, 6733, 6735, 6737, 7927, 6583, 6575, 773, 1635, 1637, 1639, 1641, 1643, 1645, 2550, 2552, 554, 2556, 2558, 2560, 2562, 2564, 2566, 2568, 2570, 2572, 4202, 4657, 6465 };
		public static Int32[] legs = new Int32[] { 1073, 13592, 14504, 14508, 14511, 5042, 5044, 5046, 5040, 5038, 5036, 6787, 6752, 6067, 6353, 6363, 6373, 6343, 548, 428, 542, 665, 6627, 7116, 7126, 7132, 7138, 1095, 1097, 1099, 6324, 7366, 7368, 2898, 2908, 2918, 2928, 293, 1845, 646, 648, 650, 652, 654, 7593, 4300, 1835, 538, 6655, 1033, 6141, 6135, 7382, 7384, 7378, 380, 5555, 7386, 7388, 7398, 4759, 6386, 6390, 2497, 2495, 2493, 1099, 4113, 4103, 4093, 6924, 687, 6185, 6181, 3059, 4510, 4505, 4070, 6108, 538, 542, 548, 1011, 1013, 1015, 1067, 1069, 1071, 073, 1075, 1077, 1079, 1081, 1083, 1085, 1087, 1089, 1091, 1093, 2585, 2593, 2601, 2609, 2617, 225, 2655, 2663, 2671, 3059, 3389, 3472, 3473, 3474, 3475, 3476, 3477, 3478, 3479, 3480, 3483, 345, 3795, 4087, 4585, 4712, 4714, 4722, 4730, 4738, 4751, 4759, 4874, 4875, 4876, 4877, 4898, 489, 4900, 4901, 4922, 4923, 4924, 4925, 4946, 4947, 4948, 4949, 4970, 4971, 4972, 4973, 4994, 49954996, 4997, 5048, 5050, 5052, 5576, 6107, 6130, 6187, 6390, 6386, 6390, 6396, 6404, 6809 };

		public static Int32[] crackers = new Int32[] { 1038, 1040, 1042, 1044, 1046, 1048 };
		/* Catherby, relekka and fishing guild - ID 5 */
		public static Int32[] fishing_big_net = new Int32[] { 353, 407, 405, 401, 341, 363 };
		public static Int32[] fishing_big_net_lvl = new Int32[] { 16, 16, 16, 16, 23, 46 };
		public static Int32[] fishing_big_net_xp = new Int32[] { 20, 10, 10, 1, 45, 100 };
		/* Any River - ID 4 */
		public static Int32[] fishing_fly = new Int32[] { 335, 349, 331 };
		public static Int32[] fishing_fly_lvl = new Int32[] { 20, 25, 30 };
		public static Int32[] fishing_fly_xp = new Int32[] { 50, 60, 70 };
		/* karamja, fishing guild, caatherby and rellekka - ID 6 */
		public static Int32[] fishing_harpoon = new Int32[] { 359, 371 };

		public static Int32[] fishing_harpoon_lvl = new Int32[] { 35, 50 };
		public static Int32[] fishing_harpoon_xp = new Int32[] { 80, 100 };
		/* Any Sea - ID 1 */
		public static Int32[] fishing_net = new Int32[] { 317, 321 };

		public static Int32[] fishing_net_lvl = new Int32[] { 1, 15 };
		public static Int32[] fishing_net_xp = new Int32[] { 10, 40 };
		/* Any Sea - ID 2 */
		public static Int32[] fishing_rod1 = new Int32[] { 327, 345 };
		public static Int32[] fishing_rod1_lvl = new Int32[] { 5, 10 };
		public static Int32[] fishing_rod1_xp = new Int32[] { 10, 30 };
		/* Lumby Swamp - ID 3 */
		public static Int32[] fishing_rod2 = new Int32[] { 3379, 5001 };
		public static Int32[] fishing_rod2_lvl = new Int32[] { 28, 36 };
		public static Int32[] fishing_rod2_xp = new Int32[] { 65, 80 };
		public static Boolean[] itemIsNote = new Boolean[19999];
		public static Boolean[] itemSellable = new Boolean[19999];
		public static Boolean[] itemStackable = new Boolean[19999];
		public static Boolean[] itemTradeable = new Boolean[19999];
		public static Boolean[] itemTwoHanded = new Boolean[19999];
		public static Int32[] normal_gems = new Int32[] { 1623, 1621, 1619, 1617 };
		public static Int32[] shilo_gems = new Int32[] { 1623, 1621, 1619, 1617, 1625, 1627, 1629 };

		public static Int32[][][] smithing_frame = new Int32[][][]
		{
			new Int32[][]
			{
					new Int32[] { 1205, 1, 1, 1, 1125, 1094 },
					new Int32[] { 1351, 1, 1, 1, 1126, 1091 },
					new Int32[] { 1422, 1, 2, 1, 1129, 1093 },
					new Int32[] { 1139, 1, 3, 1, 1127, 1102 },
					new Int32[] { 1277, 1, 3, 1, 1128, 1085 },
					new Int32[] { 819, 10, 4, 1, 1124, 1107 },
					new Int32[] { 4819, 15, 4, 1, 13357, 13358 },
					new Int32[] { 39, 15, 5, 1, 1130, 1108 },
					new Int32[] { 1321, 1, 5, 2, 1116, 1087 },
					new Int32[] { 1291, 1, 6, 2, 1089, 1086 },
					new Int32[] { 1155, 1, 7, 2, 1113, 1103 },
					new Int32[] { 864, 5, 7, 1, 1131, 1106 },
					new Int32[] { 1173, 1, 8, 2, 1114, 1104 },
					new Int32[] { 1337, 1, 9, 3, 1118, 1083 },
					new Int32[] { 1375, 1, 10, 3, 1095, 1092 },
					new Int32[] { 1103, 1, 11, 3, 1109, 1098 },
					new Int32[] { 1189, 1, 12, 3, 1115, 1105 },
					new Int32[] { 3095, 1, 13, 2, 8428, 8429 },
					new Int32[] { 1307, 1, 14, 3, 1090, 1088 },
					new Int32[] { 1087, 1, 16, 3, 1111, 1100 },
					new Int32[] { 1075, 1, 16, 3, 1110, 1099 },
					new Int32[] { 1117, 1, 18, 5, 1112, 1101 },/* Specials */
					new Int32[] { 1794, 1, 4, 1, 1132, 1096 }
			},
			new Int32[][]
			{
					new Int32[] { 1203, 1, 15, 1, 1125, 1094 },
					new Int32[] { 1349, 1, 16, 1, 1126, 1091 },
					new Int32[] { 1420, 1, 17, 1, 1129, 1093 },
					new Int32[] { 1137, 1, 18, 1, 1127, 1102 },
					new Int32[] { 1279, 1, 19, 1, 1128, 1085 },
					new Int32[] { 820, 10, 19, 1, 1124, 1107 },
					new Int32[] { 4820, 15, 19, 1, 13357, 13358 },
					new Int32[] { 40, 15, 20, 1, 1130, 1108 },
					new Int32[] { 1323, 1, 20, 2, 1116, 1087 },
					new Int32[] { 1293, 1, 21, 2, 1089, 1086 },
					new Int32[] { 1153, 1, 22, 2, 1113, 1103 },
					new Int32[] { 863, 5, 22, 1, 1131, 1106 },
					new Int32[] { 1175, 1, 23, 2, 1114, 1104 },
					new Int32[] { 1335, 1, 24, 3, 1118, 1083 },
					new Int32[] { 1363, 1, 25, 3, 1095, 1092 },
					new Int32[] { 1101, 1, 26, 3, 1109, 1098 },
					new Int32[] { 1191, 1, 27, 3, 1115, 1105 },
					new Int32[] { 3096, 1, 28, 2, 8428, 8429 },
					new Int32[] { 1309, 1, 29, 3, 1090, 1088 },
					new Int32[] { 1081, 1, 31, 3, 1111, 1100 },
					new Int32[] { 1067, 1, 31, 3, 1110, 1099 },
					new Int32[] { 1115, 1, 33, 5, 1112, 1101 },/* Specials */
					new Int32[] { 4540, 1, 26, 1, 11459, 11461 }
			},
			new Int32[][]
			{
					new Int32[] { 1207, 1, 30, 1, 1125, 1094 },
					new Int32[] { 1353, 1, 31, 1, 1126, 1091 },
					new Int32[] { 1424, 1, 32, 1, 1129, 1093 },
					new Int32[] { 1141, 1, 33, 1, 1127, 1102 },
					new Int32[] { 1281, 1, 34, 1, 1128, 1085 },
					new Int32[] { 821, 10, 34, 1, 1124, 1107 },
					new Int32[] { 1539, 15, 34, 1, 13357, 13358 },
					new Int32[] { 41, 15, 35, 1, 1130, 1108 },
					new Int32[] { 1325, 1, 35, 2, 1116, 1087 },
					new Int32[] { 1295, 1, 36, 2, 1089, 1086 },
					new Int32[] { 1157, 1, 37, 2, 1113, 1103 },
					new Int32[] { 865, 5, 37, 1, 1131, 1106 },
					new Int32[] { 1177, 1, 38, 2, 1114, 1104 },
					new Int32[] { 1339, 1, 39, 3, 1118, 1083 },
					new Int32[] { 1365, 1, 40, 3, 1095, 1092 },
					new Int32[] { 1105, 1, 41, 3, 1109, 1098 },
					new Int32[] { 1193, 1, 42, 3, 1115, 1105 },
					new Int32[] { 3097, 1, 43, 2, 8428, 8429 },
					new Int32[] { 1311, 1, 44, 3, 1090, 1088 },
					new Int32[] { 1083, 1, 46, 3, 1111, 1100 },
					new Int32[] { 1069, 1, 46, 3, 1110, 1099 },
					new Int32[] { 1119, 1, 48, 5, 1112, 1101 },/* Specials */
					new Int32[] { 4544, 1, 49, 1, 11459, 11461 },
					new Int32[] { 2370, 1, 36, 1, 1135, 1134 }
			},
			new Int32[][]
			{
					new Int32[] { 1209, 1, 50, 1, 1125, 1094 },
					new Int32[] { 1355, 1, 51, 1, 1126, 1091 },
					new Int32[] { 1428, 1, 52, 1, 1129, 1093 },
					new Int32[] { 1143, 1, 53, 1, 1127, 1102 },
					new Int32[] { 1285, 1, 53, 1, 1128, 1085 },
					new Int32[] { 822, 10, 54, 1, 1124, 1107 },
					new Int32[] { 4822, 15, 54, 1, 13357, 13358 },
					new Int32[] { 42, 15, 55, 1, 1130, 1108 },
					new Int32[] { 1329, 1, 55, 2, 1116, 1087 },
					new Int32[] { 1299, 1, 56, 2, 1089, 1086 },
					new Int32[] { 1159, 1, 57, 2, 1113, 1103 },
					new Int32[] { 866, 5, 57, 1, 1131, 1106 },
					new Int32[] { 1181, 1, 58, 2, 1114, 1104 },
					new Int32[] { 1343, 1, 59, 3, 1118, 1083 },
					new Int32[] { 1369, 1, 60, 3, 1095, 1092 },
					new Int32[] { 1109, 1, 61, 3, 1109, 1098 },
					new Int32[] { 1197, 1, 62, 3, 1115, 1105 },
					new Int32[] { 3099, 1, 63, 2, 8428, 8429 },
					new Int32[] { 1315, 1, 64, 3, 1090, 1088 },
					new Int32[] { 1085, 1, 66, 3, 1111, 1100 },
					new Int32[] { 1071, 1, 66, 3, 1110, 1099 },
					new Int32[] { 1121, 1, 68, 5, 1112, 1101 }
			},
			new Int32[][]
			{
					new Int32[] { 1211, 1, 70, 1, 1125, 1094 },
					new Int32[] { 1357, 1, 71, 1, 1126, 1091 },
					new Int32[] { 1430, 1, 72, 1, 1129, 1093 },
					new Int32[] { 1145, 1, 73, 1, 1127, 1102 },
					new Int32[] { 1287, 1, 74, 1, 1128, 1085 },
					new Int32[] { 823, 10, 74, 1, 1124, 1107 },
					new Int32[] { 4823, 15, 74, 1, 13357, 13358 },
					new Int32[] { 43, 15, 75, 1, 1130, 1108 },
					new Int32[] { 1331, 1, 75, 2, 1116, 1087 },
					new Int32[] { 1301, 1, 76, 2, 1089, 1086 },
					new Int32[] { 1161, 1, 77, 2, 1113, 1103 },
					new Int32[] { 867, 5, 77, 1, 1131, 1106 },
					new Int32[] { 1183, 1, 78, 2, 1114, 1104 },
					new Int32[] { 1345, 1, 79, 3, 1118, 1083 },
					new Int32[] { 1371, 1, 80, 3, 1095, 1092 },
					new Int32[] { 1111, 1, 81, 3, 1109, 1098 },
					new Int32[] { 1199, 1, 82, 3, 1115, 1105 },
					new Int32[] { 3100, 1, 83, 2, 8428, 8429 },
					new Int32[] { 1317, 1, 84, 3, 1090, 1088 },
					new Int32[] { 1091, 1, 86, 3, 1111, 1100 },
					new Int32[] { 1073, 1, 86, 3, 1110, 1099 },
					new Int32[] { 1123, 1, 88, 5, 1112, 1101 }
			},
			new Int32[][]
			{
					new Int32[] { 1213, 1, 85, 1, 1125, 1094 },
					new Int32[] { 1359, 1, 86, 1, 1126, 1091 },
					new Int32[] { 1432, 1, 87, 1, 1129, 1093 },
					new Int32[] { 1147, 1, 88, 1, 1127, 1102 },
					new Int32[] { 1289, 1, 89, 1, 1128, 1085 },
					new Int32[] { 824, 10, 89, 1, 1124, 1107 },
					new Int32[] { 4824, 15, 89, 1, 13357, 13358 },
					new Int32[] { 44, 15, 90, 1, 1130, 1108 },
					new Int32[] { 1333, 1, 90, 2, 1116, 1087 },
					new Int32[] { 1303, 1, 91, 2, 1089, 1086 },
					new Int32[] { 1163, 1, 92, 2, 1113, 1103 },
					new Int32[] { 868, 5, 92, 1, 1131, 1106 },
					new Int32[] { 1185, 1, 93, 2, 1114, 1104 },
					new Int32[] { 1347, 1, 94, 3, 1118, 1083 },
					new Int32[] { 1373, 1, 95, 3, 1095, 1092 },
					new Int32[] { 1113, 1, 96, 3, 1109, 1098 },
					new Int32[] { 1201, 1, 97, 3, 1115, 1105 },
					new Int32[] { 3101, 1, 98, 2, 8428, 8429 },
					new Int32[] { 1319, 1, 99, 3, 1090, 1088 },
					new Int32[] { 1093, 1, 99, 3, 1111, 1100 },
					new Int32[] { 1079, 1, 99, 3, 1110, 1099 },
					new Int32[] { 1127, 1, 99, 5, 1112, 1101 }
			}
	// 0 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23
	// dagger axe mace medium sword dart tips nails arrow heads scimitar long sword
	// full helmet knives square warhammer battle axe chain kite claws 2-handed
	// skirt legs body lantern/wire studs
	};

		public static Int32[][] SmithingItems = new Int32[5][];//5;
		static Item( )
		{
			var counter = 0;
			Int32 c;
			FileStream dataIn = null;

			try
			{
				using ( dataIn = File.Open( "data/stackable.dat", FileMode.Open ) )
				{
					while ( ( c = dataIn.ReadByte() ) != -1 )
					{
						if ( c == 0 )
							itemStackable[counter] = false;
						else
							itemStackable[counter] = true;
						counter++;
					}
				}
				Int32[] runeId = { 6430, 6432, 565, 6428, 6422, 566, 6434, 6424 };
				for ( var i = 0; i < runeId.Length; i++ )
				{
					itemStackable[runeId[i]] = true;
				}
			}
			catch ( IOException e )
			{
				Console.WriteLine( "Critical error while loading stackabledata! Trace:" );
				//e.printStackTrace();
			}

			counter = 0;
			try
			{
				using ( dataIn = File.Open( "data/notes.dat", FileMode.Open ) )
				{
					while ( ( c = dataIn.ReadByte() ) != -1 )
					{
						if ( c == 0 )
							itemIsNote[counter] = true;
						else
							itemIsNote[counter] = false;
						counter++;
					}
				}
			}
			catch ( IOException e )
			{
				Console.WriteLine( "Critical error while loading notedata! Trace:" );
				//e.printStackTrace();
			}

			counter = 0;
			try
			{
				using ( dataIn = File.Open( "data/twohanded.dat", FileMode.Open ) )
				{
					while ( ( c = dataIn.ReadByte() ) != -1 )
					{
						if ( c == 0 )
							itemTwoHanded[counter] = false;
						else
							itemTwoHanded[counter] = true;
						counter++;
					}
				}
			}
			catch ( IOException e )
			{
				Console.WriteLine( "Critical error while loading twohanded! Trace:" );
				//e.printStackTrace();
			}

			counter = 0;
			try
			{
				using ( dataIn = File.Open( "data/tradeable.dat", FileMode.Open ) )
				{
					while ( ( c = dataIn.ReadByte() ) != -1 )
					{
						if ( c == 0 )
							itemTradeable[counter] = false;
						else
							itemTradeable[counter] = true;
						counter++;
					}
				}
			}
			catch ( IOException e )
			{
				Console.WriteLine( "Critical error while loading tradeable! Trace:" );
				//e.printStackTrace();
			}

			counter = 0;
			try
			{
				using ( dataIn = File.Open( "data/sellable.dat", FileMode.Open ) )
				{
					while ( ( c = dataIn.ReadByte() ) != -1 )
					{
						if ( c == 0 )
							itemSellable[counter] = true;
						else
							itemSellable[counter] = false;
						counter++;
					}
				}
				itemSellable[1543] = false;
				itemSellable[1544] = false;
			}
			catch ( IOException e )
			{
				Console.WriteLine( "Critical error while loading sellable! Trace:" );
				//e.printStackTrace();
			}
		}

		public static Boolean isFullHelm( Int32 itemID )
		{
			foreach ( var element in fullHelm )
				if ( element == itemID )
					return true;
			return false;
		}

		public static Boolean isFullMask( Int32 itemID )
		{
			foreach ( var element in fullMask )
				if ( element == itemID )
					return true;
			return false;
		}

		public static Boolean isPlate( Int32 itemID )
		{
			foreach ( var element in platebody )
				if ( element == itemID )
					return true;
			return false;
		}

		public static Int32 randomAmulet( )
		{
			return amulets[( Int32 ) ( MathHelper.Random() * amulets.Length )];
		}

		public static Int32 randomArrows( )
		{
			return arrows[( Int32 ) ( MathHelper.Random() * arrows.Length )];
		}

		public static Int32 randomBody( )
		{
			return body[( Int32 ) ( MathHelper.Random() * body.Length )];
		}

		public static Int32 randomBoots( )
		{
			return boots[( Int32 ) ( MathHelper.Random() * boots.Length )];
		}

		public static Int32 randomCape( )
		{
			return capes[( Int32 ) ( MathHelper.Random() * capes.Length )];
		}

		public static Int32 randomGloves( )
		{
			return gloves[( Int32 ) ( MathHelper.Random() * gloves.Length )];
		}

		public static Int32 randomHat( )
		{
			return hats[( Int32 ) ( MathHelper.Random() * hats.Length )];
		}

		public static Int32 randomLegs( )
		{
			return legs[( Int32 ) ( MathHelper.Random() * legs.Length )];
		}

		public static Int32 randomNGems( )
		{
			return normal_gems[( Int32 ) ( MathHelper.Random() * normal_gems.Length )];
		}

		public static Int32 randomPHat( )
		{
			return crackers[( Int32 ) ( MathHelper.Random() * crackers.Length )];
		}

		public static Int32 randomRing( )
		{
			return rings[( Int32 ) ( MathHelper.Random() * rings.Length )];
		}

		public static Int32 randomSGems( )
		{
			return shilo_gems[( Int32 ) ( MathHelper.Random() * shilo_gems.Length )];
		}

		public static Int32 randomShield( )
		{
			return shields[( Int32 ) ( MathHelper.Random() * shields.Length )];
		}
	}
}
