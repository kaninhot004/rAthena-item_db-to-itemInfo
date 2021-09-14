using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
using System.IO;
using System.Text;

public class ItemGenerator : MonoBehaviour
{
    [SerializeField] List<string> allWeaponSubType = new List<string>() { "Dagger", "1hSword", "2hSword", "1hSpear", "2hSpear", "1hAxe", "2hAxe", "Mace", "Staff", "Bow", "Knuckle", "Musical", "Whip", "Book", "Katar", "Revolver", "Rifle", "Gatling", "Shotgun", "Grenade", "Huuma", "2hStaff" };
    [SerializeField] List<string> allAmmoSubType = new List<string>() { "Arrow", "Dagger", "Bullet", "Shell", "Grenade", "Shuriken", "Kunai", "CannonBall", "ThrowWeapon" };

    [SerializeField] List<string> allWeaponLocation = new List<string>() { "Right_Hand", "Both_Hand" };
    [SerializeField] List<string> allShieldLocation = new List<string>() { "Left_Hand" };
    [SerializeField] List<string> allArmorLocation = new List<string>() { "Head_Top", "Head_Mid", "Head_Low", "Armor", "Garment", "Shoes", "Right_Accessory", "Left_Accessory", "Both_Accessory" };
    [SerializeField] List<string> allAmmoLocation = new List<string>() { "Ammo" };

    [SerializeField] List<int> allShieldView = new List<int>() { 1, 2, 3, 4 };
    [SerializeField] List<int> allRobeView = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110 };
    [SerializeField] List<int> allHeadgearView = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143, 144, 145, 146, 147, 148, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160, 161, 162, 163, 164, 165, 166, 167, 168, 169, 170, 171, 172, 173, 174, 175, 176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186, 187, 188, 189, 190, 191, 192, 193, 194, 195, 196, 197, 198, 199, 200, 201, 202, 203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 214, 215, 216, 217, 218, 219, 220, 221, 222, 223, 224, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239, 240, 241, 242, 243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253, 254, 255, 256, 257, 258, 259, 260, 261, 262, 263, 264, 265, 266, 267, 268, 269, 270, 271, 272, 273, 274, 275, 276, 277, 278, 279, 280, 281, 282, 283, 284, 285, 286, 287, 288, 289, 290, 291, 292, 293, 294, 295, 296, 297, 298, 299, 300, 301, 302, 303, 304, 305, 306, 307, 308, 309, 310, 311, 312, 313, 314, 315, 316, 317, 318, 319, 320, 321, 322, 323, 324, 325, 326, 327, 328, 329, 330, 331, 332, 333, 334, 335, 336, 337, 338, 339, 340, 341, 342, 343, 344, 345, 346, 347, 348, 349, 350, 351, 352, 353, 354, 355, 356, 357, 358, 359, 360, 361, 362, 363, 364, 365, 366, 367, 368, 369, 370, 371, 372, 373, 374, 375, 376, 377, 378, 379, 380, 381, 382, 383, 384, 385, 386, 387, 388, 389, 390, 391, 392, 393, 394, 395, 396, 397, 398, 399, 400, 401, 402, 403, 404, 405, 406, 407, 408, 409, 410, 411, 412, 413, 414, 415, 416, 417, 418, 419, 420, 421, 422, 423, 424, 425, 426, 427, 428, 429, 430, 431, 432, 433, 434, 435, 436, 437, 438, 439, 440, 441, 442, 443, 444, 445, 446, 447, 448, 449, 450, 451, 452, 453, 454, 455, 456, 457, 458, 459, 460, 461, 462, 463, 464, 465, 466, 467, 468, 469, 470, 471, 472, 473, 474, 475, 476, 477, 478, 479, 480, 481, 482, 483, 484, 485, 486, 487, 488, 489, 490, 491, 492, 493, 494, 495, 496, 497, 498, 499, 500, 501, 502, 503, 504, 505, 506, 507, 508, 509, 510, 511, 512, 513, 514, 515, 516, 517, 518, 519, 520, 521, 522, 523, 524, 525, 526, 527, 528, 529, 530, 531, 532, 533, 534, 535, 536, 537, 538, 539, 540, 541, 542, 543, 544, 545, 546, 547, 548, 549, 550, 551, 552, 553, 554, 555, 556, 557, 558, 559, 560, 561, 562, 563, 564, 565, 566, 567, 568, 569, 570, 571, 572, 573, 574, 575, 576, 577, 578, 579, 580, 581, 582, 583, 584, 585, 586, 587, 588, 589, 590, 591, 592, 593, 594, 595, 596, 597, 598, 599, 600, 601, 602, 603, 604, 605, 606, 607, 608, 609, 610, 611, 612, 613, 614, 615, 616, 617, 618, 619, 620, 621, 622, 623, 624, 625, 626, 627, 628, 629, 630, 631, 632, 633, 634, 635, 636, 637, 638, 639, 640, 641, 642, 643, 644, 645, 646, 647, 648, 649, 650, 651, 652, 653, 654, 655, 656, 657, 658, 659, 660, 661, 662, 663, 664, 665, 666, 667, 668, 669, 670, 671, 672, 673, 674, 675, 676, 677, 678, 679, 680, 681, 682, 683, 684, 685, 686, 687, 688, 689, 690, 691, 692, 693, 694, 695, 696, 697, 698, 699, 700, 701, 702, 703, 704, 705, 706, 707, 708, 709, 710, 711, 712, 713, 714, 715, 716, 717, 718, 719, 720, 721, 722, 723, 724, 725, 726, 727, 728, 729, 730, 731, 732, 733, 734, 735, 736, 737, 738, 739, 740, 741, 742, 743, 744, 745, 746, 747, 748, 749, 750, 751, 752, 753, 754, 755, 756, 757, 758, 759, 760, 761, 762, 763, 764, 765, 766, 767, 768, 769, 770, 771, 772, 773, 774, 775, 776, 777, 778, 779, 780, 781, 782, 783, 784, 785, 786, 787, 788, 789, 790, 791, 792, 793, 794, 795, 796, 797, 798, 799, 800, 801, 802, 803, 804, 805, 806, 807, 808, 809, 810, 811, 812, 813, 814, 815, 816, 817, 818, 819, 820, 821, 822, 823, 824, 825, 826, 827, 828, 829, 830, 831, 832, 836, 837, 838, 839, 840, 841, 842, 843, 844, 845, 846, 847, 848, 849, 850, 851, 852, 853, 854, 855, 856, 857, 858, 859, 860, 861, 862, 863, 864, 865, 866, 867, 868, 869, 870, 871, 872, 873, 874, 875, 876, 877, 878, 879, 882, 884, 885, 886, 887, 888, 889, 890, 891, 892, 893, 894, 895, 896, 897, 898, 899, 900, 901, 902, 903, 904, 905, 906, 907, 908, 909, 910, 911, 912, 913, 914, 915, 916, 917, 918, 919, 920, 921, 922, 923, 924, 925, 926, 927, 928, 929, 930, 931, 932, 933, 934, 935, 936, 937, 938, 939, 940, 941, 942, 943, 944, 945, 946, 947, 948, 949, 950, 951, 952, 953, 954, 955, 956, 957, 958, 959, 960, 961, 962, 963, 964, 965, 966, 967, 968, 969, 970, 971, 972, 973, 974, 975, 976, 977, 978, 981, 982, 983, 984, 985, 986, 987, 988, 989, 990, 991, 992, 993, 994, 995, 996, 997, 998, 999, 1000, 1001, 1002, 1003, 1004, 1005, 1006, 1007, 1008, 1009, 1010, 1011, 1012, 1013, 1014, 1015, 1016, 1017, 1018, 1019, 1020, 1021, 1022, 1023, 1024, 1025, 1026, 1027, 1028, 1029, 1030, 1031, 1032, 1033, 1034, 1035, 1036, 1037, 1038, 1039, 1040, 1041, 1042, 1043, 1044, 1045, 1046, 1047, 1048, 1049, 1050, 1051, 1052, 1053, 1054, 1055, 1056, 1057, 1058, 1059, 1062, 1063, 1064, 1065, 1066, 1067, 1068, 1069, 1070, 1071, 1072, 1073, 1075, 1076, 1077, 1078, 1079, 1080, 1081, 1082, 1083, 1084, 1085, 1086, 1087, 1088, 1089, 1090, 1091, 1092, 1093, 1094, 1095, 1096, 1097, 1098, 1099, 1100, 1101, 1102, 1103, 1104, 1105, 1106, 1107, 1108, 1109, 1110, 1111, 1112, 1113, 1114, 1115, 1116, 1117, 1118, 1119, 1120, 1121, 1122, 1123, 1124, 1125, 1126, 1127, 1128, 1129, 1130, 1131, 1132, 1133, 1134, 1135, 1136, 1137, 1138, 1139, 1140, 1141, 1142, 1143, 1144, 1145, 1146, 1147, 1148, 1149, 1150, 1151, 1152, 1153, 1154, 1155, 1156, 1157, 1158, 1159, 1160, 1161, 1162, 1164, 1169, 1170, 1171, 1172, 1173, 1174, 1175, 1176, 1180, 1181, 1183, 1184, 1185, 1186, 1188, 1189, 1190, 1191, 1192, 1193, 1194, 1195, 1196, 1197, 1198, 1199, 1200, 1201, 1202, 1203, 1204, 1205, 1206, 1207, 1208, 1209, 1210, 1211, 1212, 1213, 1214, 1215, 1216, 1217, 1218, 1219, 1220, 1221, 1222, 1223, 1224, 1227, 1228, 1229, 1230, 1231, 1232, 1233, 1234, 1235, 1236, 1237, 1238, 1239, 1240, 1241, 1242, 1243, 1244, 1245, 1246, 1247, 1248, 1249, 1250, 1251, 1253, 1254, 1255, 1256, 1257, 1258, 1259, 1260, 1261, 1262, 1263, 1264, 1265, 1266, 1267, 1268, 1269, 1270, 1271, 1272, 1273, 1274, 1275, 1276, 1277, 1278, 1279, 1280, 1281, 1282, 1283, 1284, 1285, 1286, 1287, 1288, 1289, 1290, 1291, 1292, 1293, 1294, 1295, 1296, 1297, 1298, 1300, 1301, 1302, 1303, 1304, 1305, 1306, 1307, 1308, 1309, 1310, 1311, 1312, 1313, 1314, 1315, 1316, 1317, 1318, 1319, 1320, 1321, 1322, 1323, 1324, 1325, 1326, 1327, 1328, 1329, 1330, 1331, 1332, 1333, 1334, 1335, 1336, 1337, 1338, 1339, 1340, 1341, 1342, 1343, 1344, 1345, 1346, 1347, 1348, 1349, 1350, 1351, 1352, 1353, 1354, 1355, 1356, 1357, 1358, 1359, 1360, 1361, 1362, 1363, 1364, 1365, 1366, 1367, 1368, 1369, 1370, 1371, 1372, 1373, 1374, 1375, 1376, 1377, 1378, 1379, 1380, 1381, 1382, 1383, 1384, 1385, 1386, 1387, 1388, 1389, 1390, 1391, 1392, 1393, 1394, 1395, 1396, 1397, 1398, 1399, 1400, 1401, 1402, 1403, 1404, 1405, 1406, 1407, 1408, 1409, 1410, 1411, 1412, 1413, 1414, 1415, 1416, 1417, 1418, 1419, 1420, 1421, 1422, 1423, 1424, 1425, 1426, 1427, 1428, 1429, 1430, 1431, 1432, 1433, 1434, 1435, 1436, 1437, 1438, 1439, 1440, 1441, 1442, 1443, 1444, 1445, 1446, 1447, 1448, 1449, 1450, 1451, 1452, 1453, 1454, 1455, 1456, 1457, 1458, 1460, 1461, 1462, 1463, 1464, 1465, 1466, 1467, 1468, 1469, 1470, 1471, 1472, 1473, 1474, 1475, 1476, 1477, 1478, 1479, 1480, 1481, 1482, 1483, 1484, 1485, 1486, 1487, 1488, 1489, 1490, 1491, 1492, 1493, 1494, 1495, 1496, 1497, 1498, 1499, 1500, 1501, 1502, 1503, 1504, 1505, 1506, 1507, 1508, 1509, 1510, 1511, 1512, 1513, 1514, 1515, 1516, 1517, 1518, 1519, 1520, 1521, 1522, 1523, 1524, 1525, 1526, 1527, 1528, 1529, 1530, 1531, 1532, 1533, 1534, 1535, 1536, 1537, 1538, 1539, 1540, 1541, 1542, 1543, 1544, 1545, 1546, 1547, 1548, 1549, 1550, 1551, 1552, 1553, 1554, 1555, 1556, 1557, 1558, 1559, 1560, 1561, 1562, 1563, 1564, 1565, 1566, 1567, 1568, 1569, 1570, 1571, 1572, 1573, 1574, 1575, 1576, 1577, 1578, 1579, 1580, 1581, 1582, 1583, 1584, 1585, 1586, 1587, 1588, 1589, 1590, 1591, 1592, 1593, 1594, 1595, 1596, 1597, 1598, 1599, 1600, 1601, 1602, 1603, 1604, 1605, 1606, 1607, 1608, 1609, 1610, 1611, 1612, 1613, 1614, 1615, 1616, 1617, 1618, 1619, 1620, 1621, 1622, 1623, 1624, 1625, 1626, 1627, 1628, 1629, 1630, 1631, 1632, 1633, 1634, 1635, 1636, 1637, 1638, 1639, 1643, 1645, 1646, 1647, 1648, 1649, 1650, 1651, 1652, 1653, 1654, 1655, 1656, 1657, 1658, 1659, 1660, 1661, 1662, 1663, 1664, 1665, 1666, 1667, 1668, 1669, 1670, 1671, 1672, 1673, 1674, 1675, 1676, 1677, 1678, 1679, 1680, 1681, 1682, 1683, 1684, 1685, 1687, 1688, 1689, 1690, 1691, 1692, 1693, 1694, 1695, 1696, 1697, 1698, 1699, 1700, 1701, 1702, 1703, 1704, 1705, 1706, 1707, 1708, 1709, 1710, 1711, 1712, 1713, 1714, 1715, 1716, 1717, 1718, 1719, 1720, 1721, 1722, 1723, 1724, 1725, 1726, 1727, 1728, 1729, 1730, 1731, 1732, 1733, 1734, 1735, 1736, 1737, 1738, 1739, 1740, 1741, 1742, 1743, 1744, 1745, 1746, 1747, 1748, 1750, 1751, 1752, 1753, 1754, 1755, 1756, 1757, 1758, 1759, 1760, 1761, 1762, 1763, 1764, 1765, 1766, 1768, 1769, 1770, 1771, 1772, 1773, 1774, 1775, 1776, 1777, 1778, 1779, 1780, 1783, 1784, 1785, 1786, 1787, 1788, 1789, 1790, 1791, 1792, 1793, 1794, 1795, 1796, 1797, 1798, 1799, 1800, 1801, 1802, 1803, 1804, 1805, 1806, 1808, 1809, 1810, 1811, 1812, 1813, 1814, 1815, 1816, 1817, 1818, 1819, 1820, 1821, 1822, 1823, 1824, 1825, 1826, 1827, 1830, 1831, 1832, 1833, 1834, 1835, 1836, 1837, 1838, 1839, 1840, 1841, 1842, 1843, 1844, 1845, 1846, 1847, 1848, 1849, 1850, 1851, 1852, 1853, 1854, 1855, 1856, 1857, 1858, 1859, 1860, 1861, 1864, 1865, 1866, 1867, 1868, 1869, 1870, 1871, 1872, 1873, 1874, 1875, 1876, 1877, 1878, 1879, 1880, 1881, 1882, 1883, 1884, 1885, 1886, 1887, 1888, 1889, 1890, 1891, 1892, 1893, 1894, 1895, 1896, 1897, 1898, 1899, 1900, 1901, 1906, 1907, 1909, 1910, 1911, 1912, 1913, 1914, 1915, 1916, 1917, 1918, 1919, 1921, 1922, 1923, 1924, 1925, 1926, 1927, 1928, 1929, 1930, 1931, 1932, 1933, 1934, 1935, 1936, 1937, 1938, 1939, 1940, 1942, 1943, 1944, 1945, 1946, 1947, 1948, 1949, 1950, 1951, 1952, 1953, 1954, 1955, 1956, 1957, 1958, 1959, 1960, 1961, 1962, 1963, 1964, 1965, 1966, 1967, 1968, 1969, 1970, 1971, 1972, 1973, 1974, 1975, 1976, 1977, 1978, 1979, 1980, 1981, 1982, 1983, 1984, 1985, 1986, 1987, 1988, 1989, 1990, 1991, 1992, 1993, 1994, 1995, 1996, 1998, 1999, 2000, 2001, 2002, 2003, 2004, 2005, 2006, 2007, 2010, 2011, 2015, 2016, 2017, 2018, 2019, 2020, 2023, 2024, 2025, 2026, 2027, 2028, 2029, 2030, 2031, 2032, 2033, 2034, 2035, 2036, 2037, 2038, 2039, 2040, 2041, 2042, 2043, 2044, 2045, 2046, 2047, 2048, 2049, 2050, 2051, 2052, 2053, 2054, 2055, 2056, 2057, 2058, 2059, 2060, 2062, 2063, 2064, 2065, 2066, 2067, 2068, 2069, 2070, 2071, 2072, 2073, 2074, 2077, 2078, 2079, 2081, 2082, 2083, 2084, 2085, 2087, 2088, 2089, 2090, 2091, 2095, 2096, 2097, 2098, 2099, 2102, 2103, 2104, 2105, 2106, 2107, 2108, 2109, 2110, 2111, 2112, 2113, 2114, 2115, 2116, 2118, 2119, 2129, 2130, 2131, 2132, 2135, 2142 };
    public List<int> m_AllHeadgearView { get { return allHeadgearView; } }
    [SerializeField] List<int> allDaggerView = new List<int>() { 1, 31, 32, 33, 34, 35, 36, 37, 38 };
    [SerializeField] List<int> all1hSwordView = new List<int>() { 2, 39, 40, 41, 42, 43, 44, 45, 46, 47 };
    [SerializeField] List<int> all2hSwordView = new List<int>() { 3, 48, 49, 50, 51 };
    [SerializeField] List<int> all1hSpearView = new List<int>() { 4 };
    [SerializeField] List<int> all2hSpearView = new List<int>() { 5, 52, 53, 54, 55, 56, 57 };
    [SerializeField] List<int> all1hAxeView = new List<int>() { 6 };
    [SerializeField] List<int> all2hAxeView = new List<int>() { 7, 58, 59, 60, 61 };
    [SerializeField] List<int> all1hMaceView = new List<int>() { 8, 62, 63, 64, 65, 66, 67, 68, 98 };
    [SerializeField] List<int> all2hMaceView = new List<int>() { 9 };
    [SerializeField] List<int> all1hRodView = new List<int>() { 10, 69, 70, 71, 72, 99, 100, 101, 102 };
    [SerializeField] List<int> allBowView = new List<int>() { 11, 73, 74, 75, 76, 77 };
    [SerializeField] List<int> allKnuckleView = new List<int>() { 12, 78, 79, 80, 81, 82, 83, 84, 85 };
    [SerializeField] List<int> allInstrumentView = new List<int>() { 13 };
    [SerializeField] List<int> allWhipView = new List<int>() { 14, 86, 87, 88 };
    [SerializeField] List<int> allBookView = new List<int>() { 15, 89, 90, 91, 92, 93, 94, 95 };
    [SerializeField] List<int> allKatarView = new List<int>() { 16 };
    [SerializeField] List<int> allHandgunView = new List<int>() { 17 };
    [SerializeField] List<int> allRifleView = new List<int>() { 18 };
    [SerializeField] List<int> allGatlingView = new List<int>() { 19 };
    [SerializeField] List<int> allShotgunView = new List<int>() { 20 };
    [SerializeField] List<int> allGrenadeLauncherView = new List<int>() { 21 };
    [SerializeField] List<int> allHuumaView = new List<int>() { 22 };
    [SerializeField] List<int> all2hRodView = new List<int>() { 23, 96, 97 };

    [SerializeField] List<string> allBonus = new List<string>() { "bonus bStr,{n};", "bonus bAgi,{n};", "bonus bVit,{n};", "bonus bInt,{n};", "bonus bDex,{n};", "bonus bLuk,{n};", "bonus bMaxHPrate,{n};", "bonus bMaxSPrate,{n};", "bonus bBaseAtk,{n};", "bonus bAtk2,{n};", "bonus bWeaponAtkRate,{n};", "bonus bMatk,{n};", "bonus bMatkRate,{n};", "bonus bWeaponMatkRate,{n};", "bonus bDef,{n20};", "bonus bDefRate,{n20};", "bonus bDef2,{n20};", "bonus bDef2Rate,{n20};", "bonus bMdef,{n20};", "bonus bMdefRate,{n20};", "bonus bMdef2,{n20};", "bonus bMdef2Rate,{n20};", "bonus bHit,{n2};", "bonus bHitRate,{n5};", "bonus bCritical,{n};", "bonus bCriticalRate,{n5};", "bonus bFlee,{n5};", "bonus bFleeRate,{n20};", "bonus bPerfectHitAddRate,{n20};", "bonus bSpeedAddRate,{n20};", "bonus bAspd,{n20};", "bonus bAspdRate,{n20};", "bonus bHPrecovRate,{n};", "bonus bSPrecovRate,{n};", "bonus bUseSPrate,-{n10};", "bonus bShortAtkRate,{n2};", "bonus bLongAtkRate,{n2};", "bonus bCritAtkRate,{n2};", "bonus bCritDefRate,{n20};", "bonus bCriticalDef,{n20};", "bonus bNearAtkDef,{n20};", "bonus bLongAtkDef,{n20};", "bonus bMagicAtkDef,{n20};", "bonus bMiscAtkDef,{n20};", "bonus bHealPower,{n};", "bonus bHealPower2,{n};", "bonus bFixedCastrate,-{n5};", "bonus bVariableCastrate,-{n5};", "bonus bFixedCast,-{t2};", "bonus bVariableCast,-{t2};", "bonus bNoCastCancel2;", "bonus bDelayrate,-{n2};", "bonus2 bAddEle,{e},{n};", "bonus2 bMagicAddEle,{e},{n};", "bonus2 bSubEle,{e},{n20};", "bonus2 bAddRace,{r},{n};", "bonus2 bMagicAddRace,{r},{n};", "bonus2 bSubRace,{r},{n20};", "bonus2 bAddClass,{c},{n};", "bonus2 bMagicAddClass,{c},{n};", "bonus2 bSubClass,{c},{n20};", "bonus2 bAddSize,{s},{n};", "bonus2 bMagicAddSize,{s},{n};", "bonus2 bSubSize,{s},{n20};", "bonus2 bMagicSubSize,{s},{n20};", "bonus bNoSizeFix;", "bonus bAtkEle,{e};", "bonus bDefEle,{e};", "bonus2 bMagicAtkEle,{e},{n};", "bonus bDefRatioAtkRace,{r};", "bonus bDefRatioAtkEle,{e};", "bonus bDefRatioAtkClass,{c};", "bonus2 bIgnoreDefRaceRate,{r},{n2};", "bonus2 bIgnoreMdefRaceRate,{r},{n2};", "bonus2 bIgnoreDefClassRate,{c},{n2};", "bonus2 bIgnoreMdefClassRate,{c},{n2};", "bonus2 bExpAddRace,{r},{n5};", "bonus2 bExpAddClass,{c},{n5};", "bonus2 bAddEff,{eff},{n};", "bonus2 bResEff,{eff},{n};", "bonus bHPDrainValue,{n};", "bonus bSPDrainValue,{n};", "bonus bShortWeaponDamageReturn,{n20};", "bonus bLongWeaponDamageReturn,{n20};", "bonus bMagicDamageReturn,{n20};", "bonus bReduceDamageReturn,{n20};", "bonus bUnstripableWeapon;", "bonus bUnstripableArmor;", "bonus bUnstripableHelm;", "bonus bUnstripableShield;", "bonus bUnstripable;", "bonus bUnbreakable,{n2};", "bonus2 bDropAddRace,{r},{n5};", "bonus2 bDropAddClass,{c},{n5};", "bonus bDoubleAddRate,{n20};", "bonus bNoKnockback;", "bonus bNoGemStone;", "bonus bIntravision;", "bonus bPerfectHide;", "bonus bNoMadoFuel;", "bonus bNoWalkDelay;" };

    [SerializeField] List<string> allEffect = new List<string>() { "Eff_Bleeding", "Eff_Blind", "Eff_Burning", "Eff_Confusion", "Eff_Crystalize", "Eff_Curse", "Eff_Fear", "Eff_Freeze", "Eff_Poison", "Eff_Silence", "Eff_Sleep", "Eff_Stone", "Eff_Stun" };

    [SerializeField] List<string> allElement = new List<string>() { "Ele_Dark", "Ele_Earth", "Ele_Fire", "Ele_Ghost", "Ele_Holy", "Ele_Neutral", "Ele_Poison", "Ele_Undead", "Ele_Water", "Ele_Wind" };

    [SerializeField] List<string> allRace = new List<string>() { "RC_Angel", "RC_Brute", "RC_DemiHuman", "RC_Demon", "RC_Dragon", "RC_Fish", "RC_Formless", "RC_Insect", "RC_Plant", "RC_Player_Human", "RC_Player_Doram", "RC_Undead" };

    [SerializeField] List<string> allClass = new List<string>() { "Class_Normal", "Class_Boss" };

    [SerializeField] List<string> allSize = new List<string>() { "Size_Small", "Size_Medium", "Size_Large" };

    // View
    int GetWeaponView(string subType)
    {
        if (subType == "Dagger") return allDaggerView[Random.Range(0, allDaggerView.Count)];
        else if (subType == "1hSword") return all1hSwordView[Random.Range(0, all1hSwordView.Count)];
        else if (subType == "2hSword") return all2hSwordView[Random.Range(0, all2hSwordView.Count)];
        else if (subType == "1hSpear") return all1hSpearView[Random.Range(0, all1hSpearView.Count)];
        else if (subType == "2hSpear") return all2hSpearView[Random.Range(0, all2hSpearView.Count)];
        else if (subType == "1hAxe") return all1hAxeView[Random.Range(0, all1hAxeView.Count)];
        else if (subType == "2hAxe") return all2hAxeView[Random.Range(0, all2hAxeView.Count)];
        else if (subType == "Mace") return all1hMaceView[Random.Range(0, all1hMaceView.Count)];
        else if (subType == "Staff") return all1hRodView[Random.Range(0, all1hRodView.Count)];
        else if (subType == "Bow") return allBowView[Random.Range(0, allBowView.Count)];
        else if (subType == "Knuckle") return allKnuckleView[Random.Range(0, allKnuckleView.Count)];
        else if (subType == "Musical") return allInstrumentView[Random.Range(0, allInstrumentView.Count)];
        else if (subType == "Whip") return allWhipView[Random.Range(0, allWhipView.Count)];
        else if (subType == "Book") return allBookView[Random.Range(0, allBookView.Count)];
        else if (subType == "Katar") return allKatarView[Random.Range(0, allKatarView.Count)];
        else if (subType == "Revolver") return allHandgunView[Random.Range(0, allHandgunView.Count)];
        else if (subType == "Rifle") return allRifleView[Random.Range(0, allRifleView.Count)];
        else if (subType == "Gatling") return allGatlingView[Random.Range(0, allGatlingView.Count)];
        else if (subType == "Shotgun") return allShotgunView[Random.Range(0, allShotgunView.Count)];
        else if (subType == "Grenade") return allGrenadeLauncherView[Random.Range(0, allGrenadeLauncherView.Count)];
        else if (subType == "Huuma") return allHuumaView[Random.Range(0, allHuumaView.Count)];
        else if (subType == "2hStaff") return all2hRodView[Random.Range(0, all2hRodView.Count)];
        return 0;
    }
    int GetArmorView(string location)
    {
        if (location == "Head_Top: true" || location == "Head_Mid: true" || location == "Head_Low: true") return allHeadgearView[Random.Range(0, allHeadgearView.Count)];
        else if (location == "Garment: true") return allRobeView[Random.Range(0, allRobeView.Count)];
        return 0;
    }
    enum GenType { Weapon, Shield, Armor, Ammo };

    string GetTierName(int id)
    {
        if (id <= startId + (itemPerTier * 1))
            return "Normal";
        else if (id <= startId + (itemPerTier * 2))
            return "Advance";
        else if (id <= startId + (itemPerTier * 3))
            return "Rare";
        else if (id <= startId + (itemPerTier * 4))
            return "Mystical";
        else if (id <= startId + (itemPerTier * 5))
            return "Legendary";
        return "Normal";
    }
    string GetTierBonusValue(int id, string location)
    {
        StringBuilder sumBonus = new StringBuilder();

        // Bonus Count
        int bonusCount = 1;
        if (id <= startId + (itemPerTier * 1))
            bonusCount = Random.Range(1, 4); // 1~3
        else if (id <= startId + (itemPerTier * 2))
            bonusCount = Random.Range(2, 7); // 2~6
        else if (id <= startId + (itemPerTier * 3))
            bonusCount = Random.Range(3, 10); // 3~9
        else if (id <= startId + (itemPerTier * 4))
            bonusCount = Random.Range(4, 13); // 4~12
        else if (id <= startId + (itemPerTier * 5))
            bonusCount = Random.Range(5, 16); // 5~15

        bool isElementalAlreadyHad = false;

        for (int i = 0; i < bonusCount; i++)
        {
            string bonus = allBonus[Random.Range(0, allBonus.Count)];

            while ((bonus == "bonus bAtkEle,{e};" || bonus == "bonus bDefEle,{e};") && isElementalAlreadyHad)
                bonus = allBonus[Random.Range(0, allBonus.Count)];

            while (bonus == "bonus bAtkEle,{e};" && (!location.Contains("Ammo:") || !location.Contains("Right_Hand:")))
                bonus = allBonus[Random.Range(0, allBonus.Count)];

            while (bonus == "bonus bDefEle,{e};" && !location.Contains("Armor:"))
                bonus = allBonus[Random.Range(0, allBonus.Count)];

            if (bonus == "bonus bAtkEle,{e};" || bonus == "bonus bDefEle,{e};")
                isElementalAlreadyHad = true;

            // Value
            int sum = 0;
            if (id <= startId + (itemPerTier * 1))
                sum = Random.Range(1, 11);
            else if (id <= startId + (itemPerTier * 2))
                sum = Random.Range(10, 26);
            else if (id <= startId + (itemPerTier * 3))
                sum = Random.Range(25, 51);
            else if (id <= startId + (itemPerTier * 4))
                sum = Random.Range(50, 101);
            else if (id <= startId + (itemPerTier * 5))
                sum = Random.Range(100, 200);
            bonus = bonus.Replace("{n20}", (sum / 20 <= 0) ? "1" : (sum / 20).ToString("f0"));
            bonus = bonus.Replace("{n10}", (sum / 10 <= 0) ? "1" : (sum / 10).ToString("f0"));
            bonus = bonus.Replace("{n5}", (sum / 5 <= 0) ? "1" : (sum / 5).ToString("f0"));
            bonus = bonus.Replace("{n2}", (sum / 2 <= 0) ? "1" : (sum / 2).ToString("f0"));
            bonus = bonus.Replace("{n}", (sum <= 0) ? "1" : sum.ToString("f0"));

            // Time
            sum = 1;
            if (id <= startId + (itemPerTier * 1))
                sum = Random.Range(1, 11);
            else if (id <= startId + (itemPerTier * 2))
                sum = Random.Range(10, 51);
            else if (id <= startId + (itemPerTier * 3))
                sum = Random.Range(50, 201);
            else if (id <= startId + (itemPerTier * 4))
                sum = Random.Range(200, 501);
            else if (id <= startId + (itemPerTier * 5))
                sum = Random.Range(500, 1001);
            bonus = bonus.Replace("{t2}", (sum <= 0) ? "1" : sum.ToString("f0"));
            bonus = bonus.Replace("{t}", ((sum * 100) <= 0) ? "1" : (sum * 100).ToString("f0"));

            // Element
            bonus = bonus.Replace("{e}", allElement[Random.Range(0, allElement.Count)]);

            // Race
            bonus = bonus.Replace("{r}", allRace[Random.Range(0, allRace.Count)]);

            // Class
            bonus = bonus.Replace("{c}", allClass[Random.Range(0, allClass.Count)]);

            // Size
            bonus = bonus.Replace("{s}", allSize[Random.Range(0, allSize.Count)]);

            // Effect
            bonus = bonus.Replace("{eff}", allEffect[Random.Range(0, allEffect.Count)]);

            sumBonus.Append("        " + bonus + "\n");
        }

        return sumBonus.ToString();
    }
    string GetTierDamageValue(int id)
    {
        // Value
        int damage = 1;
        if (id <= startId + (itemPerTier * 1))
            damage = Random.Range(25, 51); // 25~50
        else if (id <= startId + (itemPerTier * 2))
            damage = Random.Range(50, 101); // 50~100
        else if (id <= startId + (itemPerTier * 3))
            damage = Random.Range(100, 201); // 100~200
        else if (id <= startId + (itemPerTier * 4))
            damage = Random.Range(200, 301); // 200~300
        else if (id <= startId + (itemPerTier * 5))
            damage = Random.Range(300, 501); // 300~500

        return damage.ToString("f0");
    }
    string GetTierDefenseValue(int id)
    {
        // Value
        int defense = 1;
        if (id <= startId + (itemPerTier * 1))
            defense = Random.Range(1, 6); // 1~5
        else if (id <= startId + (itemPerTier * 2))
            defense = Random.Range(5, 11); // 5~10
        else if (id <= startId + (itemPerTier * 3))
            defense = Random.Range(10, 21); // 10~20
        else if (id <= startId + (itemPerTier * 4))
            defense = Random.Range(20, 36); // 20~35
        else if (id <= startId + (itemPerTier * 5))
            defense = Random.Range(35, 51); // 35~50

        return defense.ToString("f0");
    }
    string GetTierLevelRequirementValue(int id)
    {
        // Value
        int lv = 1;
        if (id <= startId + (itemPerTier * 1))
            lv = 1;
        else if (id <= startId + (itemPerTier * 2))
            lv = 30;
        else if (id <= startId + (itemPerTier * 3))
            lv = 70;
        else if (id <= startId + (itemPerTier * 4))
            lv = 110;
        else if (id <= startId + (itemPerTier * 5))
            lv = 140;

        return lv.ToString("f0");
    }
    string GetTierRangeValue(int id)
    {
        // Value
        int range = 1;
        if (id <= startId + (itemPerTier * 1))
            range = Random.Range(1, 3); // 1~2
        else if (id <= startId + (itemPerTier * 2))
            range = Random.Range(1, 4); // 1~3
        else if (id <= startId + (itemPerTier * 3))
            range = Random.Range(1, 5); // 1~4
        else if (id <= startId + (itemPerTier * 4))
            range = Random.Range(1, 6); // 1~5
        else if (id <= startId + (itemPerTier * 5))
            range = Random.Range(1, 7); // 1~6

        return range.ToString("f0");
    }

    public const int itemPerTier = 10000;
    public const int highestTier = 5;
    public const int startId = 10000000;

    /// <summary>
    /// https://stackoverflow.com/questions/4135317/make-first-letter-of-a-string-upper-case-with-maximum-performance
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    string Capitalize(string s)
    {
        if (string.IsNullOrEmpty(s))
            return string.Empty;
        char[] a = s.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }

    [Button]
    public void Generate()
    {
        var allTextAsset = File.ReadAllText(Application.dataPath + "/Assets/all_english_word.txt");
        var lines = allTextAsset.Split('\n');
        allEnglishWord = new List<string>();
        for (int i = 0; i < lines.Length; i++)
        {
            var text = lines[i];
            if (!string.IsNullOrEmpty(text) && !string.IsNullOrWhiteSpace(text))
                allEnglishWord.Add(Capitalize(text));
        }

        StringBuilder sum = new StringBuilder();

        for (int i = 0; i < (itemPerTier * highestTier) + 1; i++)
        {
            int itemType = Random.Range(0, 100);
            GenType genType = GenType.Weapon; // 45% (50~94)
            if (itemType >= 95) // 5% (95~99)
                genType = GenType.Shield;
            else if (itemType <= 49) // 50% (0~49)
                genType = GenType.Armor;

            string subType = genType == GenType.Weapon ? allWeaponSubType[Random.Range(0, allWeaponSubType.Count)] : genType == GenType.Ammo ? allAmmoSubType[Random.Range(0, allAmmoSubType.Count)] : "";
            string location = genType == GenType.Weapon ? allWeaponLocation[Random.Range(0, allWeaponLocation.Count)] + ": true" : genType == GenType.Shield ? allShieldLocation[Random.Range(0, allShieldLocation.Count)] + ": true" : genType == GenType.Armor ? allArmorLocation[Random.Range(0, allArmorLocation.Count)] + ": true" : genType == GenType.Ammo ? allAmmoLocation[Random.Range(0, allAmmoLocation.Count)] + ": true" : string.Empty;

            if (genType == GenType.Weapon)
            {
                if (subType == "2hSword" || subType == "2hSpear" || subType == "2hAxe" || subType == "Bow" || subType == "Katar" || subType == "Revolver" || subType == "Rifle" || subType == "Gatling" || subType == "Shotgun" || subType == "Grenade" || subType == "Huuma" || subType == "2hStaff")
                    location = "Both_Hand: true";

                if (subType == "Dagger" || subType == "1hSword" || subType == "1hSpear" || subType == "1hAxe" || subType == "Mace" || subType == "Whip" || subType == "Book" || subType == "Musical" || subType == "Knuckle")
                    location = "Right_Hand: true";
            }

            if (genType == GenType.Ammo)
                sum.Append(string.Format("  - Id: {0}\n    AegisName: {1}\n    Name: {2}\n    Type: {3}\n    SubType: {4}\n    Weight: {5}\n    Attack: {6}\n    MagicAttack: {7}\n    Locations:\n        {11}\n    EquipLevelMin: {15}\n    Script: |\n{14}"
                , (startId + i).ToString("f0") // ID
                     , "aegis_" + (startId + i).ToString("f0") // Aegis Name
                     , "\"[" + GetTierName(startId + i) + "]" + GetRandomEnglishWord + "\"" // Name
                     , "Ammo" // Type
                     , subType // Sub Type
                     , Random.Range(1, 100).ToString("f0") // Weight
                     , GetTierDamageValue(startId + i) // Attack
                     , GetTierDamageValue(startId + i) // Magic Attack
                     , "0" // Defense
                     , "0" // Range
                     , "0" // Slots
                     , location  // Locations
                     , "0" // Weapon Level
                     , "0" // View
                     , GetTierBonusValue(startId + i, location) // Script
                     , GetTierLevelRequirementValue(startId + i) // Level Requirement
                     ));
            else if (genType == GenType.Armor)
                sum.Append(string.Format("  - Id: {0}\n    AegisName: {1}\n    Name: {2}\n    Type: {3}\n    Weight: {5}\n    Defense: {8}\n    Locations:\n        {11}\n    EquipLevelMin: {15}\n    View: {13}\n    Script: |\n{14}"
                , (startId + i).ToString("f0") // ID
                     , "aegis_" + (startId + i).ToString("f0") // Aegis Name
                     , "\"[" + GetTierName(startId + i) + "]" + GetRandomEnglishWord + "\"" // Name
                     , "Armor" // Type
                     , subType // Sub Type
                     , Random.Range(10, 1000).ToString("f0") // Weight
                     , "0" // Attack
                     , "0" // Magic Attack
                     , GetTierDefenseValue(startId + i) // Defense
                     , "0" // Range
                     , "0" // Slots
                     , location  // Locations
                     , "0" // Weapon Level
                     , GetArmorView(location).ToString("f0") // View
                     , GetTierBonusValue(startId + i, location) // Script
                     , GetTierLevelRequirementValue(startId + i) // Level Requirement
                     ));
            else if (genType == GenType.Shield)
                sum.Append(string.Format("  - Id: {0}\n    AegisName: {1}\n    Name: {2}\n    Type: {3}\n    Weight: {5}\n    Defense: {8}\n    Locations:\n        {11}\n    EquipLevelMin: {15}\n    View: {13}\n    Script: |\n{14}"
                , (startId + i).ToString("f0") // ID
                     , "aegis_" + (startId + i).ToString("f0") // Aegis Name
                     , "\"[" + GetTierName(startId + i) + "]" + GetRandomEnglishWord + "\"" // Name
                     , "Armor" // Type
                     , subType // Sub Type
                     , Random.Range(10, 1000).ToString("f0") // Weight
                     , "0" // Attack
                     , "0" // Magic Attack
                     , GetTierDefenseValue(startId + i) // Defense
                     , "0" // Range
                     , "0" // Slots
                     , location  // Locations
                     , "0" // Weapon Level
                     , allShieldView[Random.Range(0, allShieldView.Count)].ToString("f0") // View
                     , GetTierBonusValue(startId + i, location) // Script
                     , GetTierLevelRequirementValue(startId + i) // Level Requirement
                     ));
            else if (genType == GenType.Weapon)
                sum.Append(string.Format("  - Id: {0}\n    AegisName: {1}\n    Name: {2}\n    Type: {3}\n    SubType: {4}\n    Weight: {5}\n    Attack: {6}\n    MagicAttack: {7}\n    Defense: {8}\n    Range: {9}\n{16}    Locations:\n        {11}\n    WeaponLevel: {12}\n    EquipLevelMin: {15}\n    View: {13}\n    Script: |\n{14}"
                , (startId + i).ToString("f0") // ID
                     , "aegis_" + (startId + i).ToString("f0") // Aegis Name
                     , "\"[" + GetTierName(startId + i) + "]" + GetRandomEnglishWord + "\"" // Name
                     , "Weapon" // Type
                     , subType // Sub Type
                     , Random.Range(10, 1000).ToString("f0") // Weight
                     , GetTierDamageValue(startId + i) // Attack
                     , GetTierDamageValue(startId + i) // Magic Attack
                     , "0" // Defense
                     , GetTierRangeValue(startId + i) // Range
                     , "0" // Slots
                     , location  // Locations
                     , Random.Range(1, 5).ToString("f0") // Weapon Level
                     , GetWeaponView(subType).ToString("f0") // View
                     , GetTierBonusValue(startId + i, location) // Script
                     , GetTierLevelRequirementValue(startId + i) // Level Requirement
                     , (subType == "Musical") ? "    Gender: Male\n" : (subType == "Whip") ? "    Gender: Female\n" : string.Empty // Gender
                     ));
        }

        File.WriteAllText("generatedItemDb.txt", sum.ToString(), Encoding.UTF8);
    }

    [Button]
    public void GenerateAllView()
    {
        StringBuilder sum = new StringBuilder();

        int itemName = 1;

        int id = 100000;

        for (int i = 0; i < 2146; i++)
        {
            GenType genType = GenType.Armor;

            string subType = genType == GenType.Weapon ? allWeaponSubType[Random.Range(0, allWeaponSubType.Count)] : genType == GenType.Ammo ? allAmmoSubType[Random.Range(0, allAmmoSubType.Count)] : "";
            sum.Append(string.Format(" - Id: {0}\n    AegisName: {1}\n    Name: {2}\n    Type: {3}\n    SubType: {4}\n    Weight: {5}\n    Attack: {6}\n    MagicAttack: {7}\n    Defense: {8}\n    Range: {9}\n    Slots: {10}\n    Locations:\n        {11}\n    WeaponLevel: {12}\n    View: {13}\n    Script: |\n        {14}\n"
            , id.ToString("f0") // ID
                 , "aegis_" + id.ToString("f0") // Aegis Name
                 , itemName.ToString("f0") // Name
                 , genType.ToString() // Type
                 , subType // Sub Type
                 , Random.Range(10, 10000).ToString("f0") // Weight
                 , (genType == GenType.Weapon || genType == GenType.Ammo) ? Random.Range(1, 501).ToString("f0") : "0" // Attack
                 , (genType == GenType.Weapon || genType == GenType.Ammo) ? Random.Range(1, 501).ToString("f0") : "0" // Magic Attack
                 , (genType == GenType.Armor || genType == GenType.Shield) ? Random.Range(1, 51).ToString("f0") : "0" // Defense
                 , genType == GenType.Weapon ? Random.Range(1, 6).ToString("f0") : "0" // Range
                 , "0" // Slots
                 , "Head_Top: true" // Locations
                 , genType == GenType.Weapon ? Random.Range(1, 5).ToString("f0") : "0" // Weapon Level
                 , i.ToString("f0") // View
                 , "bonus bInt,1;" // Script
                 ));
            id++;
            itemName++;
        }

        File.WriteAllText("generatedAllView.txt", sum.ToString(), Encoding.UTF8);
    }

    List<string> allEnglishWord = new List<string>();
    string GetRandomEnglishWord
    {
        get
        {
            int rand = Random.Range(1, 4);
            string sum = string.Empty;
            for (int i = 0; i < rand; i++)
            {
                if (i > 0)
                    sum += " " + allEnglishWord[Random.Range(0, allEnglishWord.Count)];
                else
                    sum += allEnglishWord[Random.Range(0, allEnglishWord.Count)];
            }
            return sum;
        }
    }
}
