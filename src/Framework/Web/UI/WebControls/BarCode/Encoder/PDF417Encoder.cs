namespace InfoControl.Web.UI.WebControls.BarCode.Encoder
{
    using System;
    using System.Collections;

    public class PDF417Encoder
    {
        private const int ABSOLUTE_MAX_TEXT_SIZE = 0x152c;
        private const int AL = 0x1c;
        private const int ALPHA = 0x10000;
        private const int AS = 0x1b;
        public double aspectRatio;
        private const int BYTE_MODE = 0x385;
        private const int BYTE_MODE_6 = 0x39c;
        private const int BYTESHIFT = 0x391;
        private int[][] CLUSTERS = new int[][] { new int[] { 
            0x1d5c0, 0x1eaf0, 0x1f57c, 0x1d4e0, 0x1ea78, 0x1f53e, 0x1a8c0, 0x1d470, 0x1a860, 0x15040, 0x1a830, 0x15020, 0x1adc0, 0x1d6f0, 0x1eb7c, 0x1ace0, 
            0x1d678, 0x1eb3e, 0x158c0, 0x1ac70, 0x15860, 0x15dc0, 0x1aef0, 0x1d77c, 0x15ce0, 0x1ae78, 0x1d73e, 0x15c70, 0x1ae3c, 0x15ef0, 0x1af7c, 0x15e78, 
            0x1af3e, 0x15f7c, 0x1f5fa, 0x1d2e0, 0x1e978, 0x1f4be, 0x1a4c0, 0x1d270, 0x1e93c, 0x1a460, 0x1d238, 0x14840, 0x1a430, 0x1d21c, 0x14820, 0x1a418, 
            0x14810, 0x1a6e0, 0x1d378, 0x1e9be, 0x14cc0, 0x1a670, 0x1d33c, 0x14c60, 0x1a638, 0x1d31e, 0x14c30, 0x1a61c, 0x14ee0, 0x1a778, 0x1d3be, 0x14e70, 
            0x1a73c, 0x14e38, 0x1a71e, 0x14f78, 0x1a7be, 0x14f3c, 0x14f1e, 0x1a2c0, 0x1d170, 0x1e8bc, 0x1a260, 0x1d138, 0x1e89e, 0x14440, 0x1a230, 0x1d11c, 
            0x14420, 0x1a218, 0x14410, 0x14408, 0x146c0, 0x1a370, 0x1d1bc, 0x14660, 0x1a338, 0x1d19e, 0x14630, 0x1a31c, 0x14618, 0x1460c, 0x14770, 0x1a3bc, 
            0x14738, 0x1a39e, 0x1471c, 0x147bc, 0x1a160, 0x1d0b8, 0x1e85e, 0x14240, 0x1a130, 0x1d09c, 0x14220, 0x1a118, 0x1d08e, 0x14210, 0x1a10c, 0x14208, 
            0x1a106, 0x14360, 0x1a1b8, 0x1d0de, 0x14330, 0x1a19c, 0x14318, 0x1a18e, 0x1430c, 0x14306, 0x1a1de, 0x1438e, 0x14140, 0x1a0b0, 0x1d05c, 0x14120, 
            0x1a098, 0x1d04e, 0x14110, 0x1a08c, 0x14108, 0x1a086, 0x14104, 0x141b0, 0x14198, 0x1418c, 0x140a0, 0x1d02e, 0x1a04c, 0x1a046, 0x14082, 0x1cae0, 
            0x1e578, 0x1f2be, 0x194c0, 0x1ca70, 0x1e53c, 0x19460, 0x1ca38, 0x1e51e, 0x12840, 0x19430, 0x12820, 0x196e0, 0x1cb78, 0x1e5be, 0x12cc0, 0x19670, 
            0x1cb3c, 0x12c60, 0x19638, 0x12c30, 0x12c18, 0x12ee0, 0x19778, 0x1cbbe, 0x12e70, 0x1973c, 0x12e38, 0x12e1c, 0x12f78, 0x197be, 0x12f3c, 0x12fbe, 
            0x1dac0, 0x1ed70, 0x1f6bc, 0x1da60, 0x1ed38, 0x1f69e, 0x1b440, 0x1da30, 0x1ed1c, 0x1b420, 0x1da18, 0x1ed0e, 0x1b410, 0x1da0c, 0x192c0, 0x1c970, 
            0x1e4bc, 0x1b6c0, 0x19260, 0x1c938, 0x1e49e, 0x1b660, 0x1db38, 0x1ed9e, 0x16c40, 0x12420, 0x19218, 0x1c90e, 0x16c20, 0x1b618, 0x16c10, 0x126c0, 
            0x19370, 0x1c9bc, 0x16ec0, 0x12660, 0x19338, 0x1c99e, 0x16e60, 0x1b738, 0x1db9e, 0x16e30, 0x12618, 0x16e18, 0x12770, 0x193bc, 0x16f70, 0x12738, 
            0x1939e, 0x16f38, 0x1b79e, 0x16f1c, 0x127bc, 0x16fbc, 0x1279e, 0x16f9e, 0x1d960, 0x1ecb8, 0x1f65e, 0x1b240, 0x1d930, 0x1ec9c, 0x1b220, 0x1d918, 
            0x1ec8e, 0x1b210, 0x1d90c, 0x1b208, 0x1b204, 0x19160, 0x1c8b8, 0x1e45e, 0x1b360, 0x19130, 0x1c89c, 0x16640, 0x12220, 0x1d99c, 0x1c88e, 0x16620, 
            0x12210, 0x1910c, 0x16610, 0x1b30c, 0x19106, 0x12204, 0x12360, 0x191b8, 0x1c8de, 0x16760, 0x12330, 0x1919c, 0x16730, 0x1b39c, 0x1918e, 0x16718, 
            0x1230c, 0x12306, 0x123b8, 0x191de, 0x167b8, 0x1239c, 0x1679c, 0x1238e, 0x1678e, 0x167de, 0x1b140, 0x1d8b0, 0x1ec5c, 0x1b120, 0x1d898, 0x1ec4e, 
            0x1b110, 0x1d88c, 0x1b108, 0x1d886, 0x1b104, 0x1b102, 0x12140, 0x190b0, 0x1c85c, 0x16340, 0x12120, 0x19098, 0x1c84e, 0x16320, 0x1b198, 0x1d8ce, 
            0x16310, 0x12108, 0x19086, 0x16308, 0x1b186, 0x16304, 0x121b0, 0x190dc, 0x163b0, 0x12198, 0x190ce, 0x16398, 0x1b1ce, 0x1638c, 0x12186, 0x16386, 
            0x163dc, 0x163ce, 0x1b0a0, 0x1d858, 0x1ec2e, 0x1b090, 0x1d84c, 0x1b088, 0x1d846, 0x1b084, 0x1b082, 0x120a0, 0x19058, 0x1c82e, 0x161a0, 0x12090, 
            0x1904c, 0x16190, 0x1b0cc, 0x19046, 0x16188, 0x12084, 0x16184, 0x12082, 0x120d8, 0x161d8, 0x161cc, 0x161c6, 0x1d82c, 0x1d826, 0x1b042, 0x1902c, 
            0x12048, 0x160c8, 0x160c4, 0x160c2, 0x18ac0, 0x1c570, 0x1e2bc, 0x18a60, 0x1c538, 0x11440, 0x18a30, 0x1c51c, 0x11420, 0x18a18, 0x11410, 0x11408, 
            0x116c0, 0x18b70, 0x1c5bc, 0x11660, 0x18b38, 0x1c59e, 0x11630, 0x18b1c, 0x11618, 0x1160c, 0x11770, 0x18bbc, 0x11738, 0x18b9e, 0x1171c, 0x117bc, 
            0x1179e, 0x1cd60, 0x1e6b8, 0x1f35e, 0x19a40, 0x1cd30, 0x1e69c, 0x19a20, 0x1cd18, 0x1e68e, 0x19a10, 0x1cd0c, 0x19a08, 0x1cd06, 0x18960, 0x1c4b8, 
            0x1e25e, 0x19b60, 0x18930, 0x1c49c, 0x13640, 0x11220, 0x1cd9c, 0x1c48e, 0x13620, 0x19b18, 0x1890c, 0x13610, 0x11208, 0x13608, 0x11360, 0x189b8, 
            0x1c4de, 0x13760, 0x11330, 0x1cdde, 0x13730, 0x19b9c, 0x1898e, 0x13718, 0x1130c, 0x1370c, 0x113b8, 0x189de, 0x137b8, 0x1139c, 0x1379c, 0x1138e, 
            0x113de, 0x137de, 0x1dd40, 0x1eeb0, 0x1f75c, 0x1dd20, 0x1ee98, 0x1f74e, 0x1dd10, 0x1ee8c, 0x1dd08, 0x1ee86, 0x1dd04, 0x19940, 0x1ccb0, 0x1e65c, 
            0x1bb40, 0x19920, 0x1eedc, 0x1e64e, 0x1bb20, 0x1dd98, 0x1eece, 0x1bb10, 0x19908, 0x1cc86, 0x1bb08, 0x1dd86, 0x19902, 0x11140, 0x188b0, 0x1c45c, 
            0x13340, 0x11120, 0x18898, 0x1c44e, 0x17740, 0x13320, 0x19998, 0x1ccce, 0x17720, 0x1bb98, 0x1ddce, 0x18886, 0x17710, 0x13308, 0x19986, 0x17708, 
            0x11102, 0x111b0, 0x188dc, 0x133b0, 0x11198, 0x188ce, 0x177b0, 0x13398, 0x199ce, 0x17798, 0x1bbce, 0x11186, 0x13386, 0x111dc, 0x133dc, 0x111ce, 
            0x177dc, 0x133ce, 0x1dca0, 0x1ee58, 0x1f72e, 0x1dc90, 0x1ee4c, 0x1dc88, 0x1ee46, 0x1dc84, 0x1dc82, 0x198a0, 0x1cc58, 0x1e62e, 0x1b9a0, 0x19890, 
            0x1ee6e, 0x1b990, 0x1dccc, 0x1cc46, 0x1b988, 0x19884, 0x1b984, 0x19882, 0x1b982, 0x110a0, 0x18858, 0x1c42e, 0x131a0, 0x11090, 0x1884c, 0x173a0, 
            0x13190, 0x198cc, 0x18846, 0x17390, 0x1b9cc, 0x11084, 0x17388, 0x13184, 0x11082, 0x13182, 0x110d8, 0x1886e, 0x131d8, 0x110cc, 0x173d8, 0x131cc, 
            0x110c6, 0x173cc, 0x131c6, 0x110ee, 0x173ee, 0x1dc50, 0x1ee2c, 0x1dc48, 0x1ee26, 0x1dc44, 0x1dc42, 0x19850, 0x1cc2c, 0x1b8d0, 0x19848, 0x1cc26, 
            0x1b8c8, 0x1dc66, 0x1b8c4, 0x19842, 0x1b8c2, 0x11050, 0x1882c, 0x130d0, 0x11048, 0x18826, 0x171d0, 0x130c8, 0x19866, 0x171c8, 0x1b8e6, 0x11042, 
            0x171c4, 0x130c2, 0x171c2, 0x130ec, 0x171ec, 0x171e6, 0x1ee16, 0x1dc22, 0x1cc16, 0x19824, 0x19822, 0x11028, 0x13068, 0x170e8, 0x11022, 0x13062, 
            0x18560, 0x10a40, 0x18530, 0x10a20, 0x18518, 0x1c28e, 0x10a10, 0x1850c, 0x10a08, 0x18506, 0x10b60, 0x185b8, 0x1c2de, 0x10b30, 0x1859c, 0x10b18, 
            0x1858e, 0x10b0c, 0x10b06, 0x10bb8, 0x185de, 0x10b9c, 0x10b8e, 0x10bde, 0x18d40, 0x1c6b0, 0x1e35c, 0x18d20, 0x1c698, 0x18d10, 0x1c68c, 0x18d08, 
            0x1c686, 0x18d04, 0x10940, 0x184b0, 0x1c25c, 0x11b40, 0x10920, 0x1c6dc, 0x1c24e, 0x11b20, 0x18d98, 0x1c6ce, 0x11b10, 0x10908, 0x18486, 0x11b08, 
            0x18d86, 0x10902, 0x109b0, 0x184dc, 0x11bb0, 0x10998, 0x184ce, 0x11b98, 0x18dce, 0x11b8c, 0x10986, 0x109dc, 0x11bdc, 0x109ce, 0x11bce, 0x1cea0, 
            0x1e758, 0x1f3ae, 0x1ce90, 0x1e74c, 0x1ce88, 0x1e746, 0x1ce84, 0x1ce82, 0x18ca0, 0x1c658, 0x19da0, 0x18c90, 0x1c64c, 0x19d90, 0x1cecc, 0x1c646, 
            0x19d88, 0x18c84, 0x19d84, 0x18c82, 0x19d82, 0x108a0, 0x18458, 0x119a0, 0x10890, 0x1c66e, 0x13ba0, 0x11990, 0x18ccc, 0x18446, 0x13b90, 0x19dcc, 
            0x10884, 0x13b88, 0x11984, 0x10882, 0x11982, 0x108d8, 0x1846e, 0x119d8, 0x108cc, 0x13bd8, 0x119cc, 0x108c6, 0x13bcc, 0x119c6, 0x108ee, 0x119ee, 
            0x13bee, 0x1ef50, 0x1f7ac, 0x1ef48, 0x1f7a6, 0x1ef44, 0x1ef42, 0x1ce50, 0x1e72c, 0x1ded0, 0x1ef6c, 0x1e726, 0x1dec8, 0x1ef66, 0x1dec4, 0x1ce42, 
            0x1dec2, 0x18c50, 0x1c62c, 0x19cd0, 0x18c48, 0x1c626, 0x1bdd0, 0x19cc8, 0x1ce66, 0x1bdc8, 0x1dee6, 0x18c42, 0x1bdc4, 0x19cc2, 0x1bdc2, 0x10850, 
            0x1842c, 0x118d0, 0x10848, 0x18426, 0x139d0, 0x118c8, 0x18c66, 0x17bd0, 0x139c8, 0x19ce6, 0x10842, 0x17bc8, 0x1bde6, 0x118c2, 0x17bc4, 0x1086c, 
            0x118ec, 0x10866, 0x139ec, 0x118e6, 0x17bec, 0x139e6, 0x17be6, 0x1ef28, 0x1f796, 0x1ef24, 0x1ef22, 0x1ce28, 0x1e716, 0x1de68, 0x1ef36, 0x1de64, 
            0x1ce22, 0x1de62, 0x18c28, 0x1c616, 0x19c68, 0x18c24, 0x1bce8, 0x19c64, 0x18c22, 0x1bce4, 0x19c62, 0x1bce2, 0x10828, 0x18416, 0x11868, 0x18c36, 
            0x138e8, 0x11864, 0x10822, 0x179e8, 0x138e4, 0x11862, 0x179e4, 0x138e2, 0x179e2, 0x11876, 0x179f6, 0x1ef12, 0x1de34, 0x1de32, 0x19c34, 0x1bc74, 
            0x1bc72, 0x11834, 0x13874, 0x178f4, 0x178f2, 0x10540, 0x10520, 0x18298, 0x10510, 0x10508, 0x10504, 0x105b0, 0x10598, 0x1058c, 0x10586, 0x105dc, 
            0x105ce, 0x186a0, 0x18690, 0x1c34c, 0x18688, 0x1c346, 0x18684, 0x18682, 0x104a0, 0x18258, 0x10da0, 0x186d8, 0x1824c, 0x10d90, 0x186cc, 0x10d88, 
            0x186c6, 0x10d84, 0x10482, 0x10d82, 0x104d8, 0x1826e, 0x10dd8, 0x186ee, 0x10dcc, 0x104c6, 0x10dc6, 0x104ee, 0x10dee, 0x1c750, 0x1c748, 0x1c744, 
            0x1c742, 0x18650, 0x18ed0, 0x1c76c, 0x1c326, 0x18ec8, 0x1c766, 0x18ec4, 0x18642, 0x18ec2, 0x10450, 0x10cd0, 0x10448, 0x18226, 0x11dd0, 0x10cc8, 
            0x10444, 0x11dc8, 0x10cc4, 0x10442, 0x11dc4, 0x10cc2, 0x1046c, 0x10cec, 0x10466, 0x11dec, 0x10ce6, 0x11de6, 0x1e7a8, 0x1e7a4, 0x1e7a2, 0x1c728, 
            0x1cf68, 0x1e7b6, 0x1cf64, 0x1c722, 0x1cf62, 0x18628, 0x1c316, 0x18e68, 0x1c736, 0x19ee8, 0x18e64, 0x18622, 0x19ee4, 0x18e62, 0x19ee2, 0x10428, 
            0x18216, 0x10c68, 0x18636, 0x11ce8, 0x10c64, 0x10422, 0x13de8, 0x11ce4, 0x10c62, 0x13de4, 0x11ce2, 0x10436, 0x10c76, 0x11cf6, 0x13df6, 0x1f7d4, 
            0x1f7d2, 0x1e794, 0x1efb4, 0x1e792, 0x1efb2, 0x1c714, 0x1cf34, 0x1c712, 0x1df74, 0x1cf32, 0x1df72, 0x18614, 0x18e34, 0x18612, 0x19e74, 0x18e32, 
            0x1bef4
         }, new int[] { 
            0x1f560, 0x1fab8, 0x1ea40, 0x1f530, 0x1fa9c, 0x1ea20, 0x1f518, 0x1fa8e, 0x1ea10, 0x1f50c, 0x1ea08, 0x1f506, 0x1ea04, 0x1eb60, 0x1f5b8, 0x1fade, 
            0x1d640, 0x1eb30, 0x1f59c, 0x1d620, 0x1eb18, 0x1f58e, 0x1d610, 0x1eb0c, 0x1d608, 0x1eb06, 0x1d604, 0x1d760, 0x1ebb8, 0x1f5de, 0x1ae40, 0x1d730, 
            0x1eb9c, 0x1ae20, 0x1d718, 0x1eb8e, 0x1ae10, 0x1d70c, 0x1ae08, 0x1d706, 0x1ae04, 0x1af60, 0x1d7b8, 0x1ebde, 0x15e40, 0x1af30, 0x1d79c, 0x15e20, 
            0x1af18, 0x1d78e, 0x15e10, 0x1af0c, 0x15e08, 0x1af06, 0x15f60, 0x1afb8, 0x1d7de, 0x15f30, 0x1af9c, 0x15f18, 0x1af8e, 0x15f0c, 0x15fb8, 0x1afde, 
            0x15f9c, 0x15f8e, 0x1e940, 0x1f4b0, 0x1fa5c, 0x1e920, 0x1f498, 0x1fa4e, 0x1e910, 0x1f48c, 0x1e908, 0x1f486, 0x1e904, 0x1e902, 0x1d340, 0x1e9b0, 
            0x1f4dc, 0x1d320, 0x1e998, 0x1f4ce, 0x1d310, 0x1e98c, 0x1d308, 0x1e986, 0x1d304, 0x1d302, 0x1a740, 0x1d3b0, 0x1e9dc, 0x1a720, 0x1d398, 0x1e9ce, 
            0x1a710, 0x1d38c, 0x1a708, 0x1d386, 0x1a704, 0x1a702, 0x14f40, 0x1a7b0, 0x1d3dc, 0x14f20, 0x1a798, 0x1d3ce, 0x14f10, 0x1a78c, 0x14f08, 0x1a786, 
            0x14f04, 0x14fb0, 0x1a7dc, 0x14f98, 0x1a7ce, 0x14f8c, 0x14f86, 0x14fdc, 0x14fce, 0x1e8a0, 0x1f458, 0x1fa2e, 0x1e890, 0x1f44c, 0x1e888, 0x1f446, 
            0x1e884, 0x1e882, 0x1d1a0, 0x1e8d8, 0x1f46e, 0x1d190, 0x1e8cc, 0x1d188, 0x1e8c6, 0x1d184, 0x1d182, 0x1a3a0, 0x1d1d8, 0x1e8ee, 0x1a390, 0x1d1cc, 
            0x1a388, 0x1d1c6, 0x1a384, 0x1a382, 0x147a0, 0x1a3d8, 0x1d1ee, 0x14790, 0x1a3cc, 0x14788, 0x1a3c6, 0x14784, 0x14782, 0x147d8, 0x1a3ee, 0x147cc, 
            0x147c6, 0x147ee, 0x1e850, 0x1f42c, 0x1e848, 0x1f426, 0x1e844, 0x1e842, 0x1d0d0, 0x1e86c, 0x1d0c8, 0x1e866, 0x1d0c4, 0x1d0c2, 0x1a1d0, 0x1d0ec, 
            0x1a1c8, 0x1d0e6, 0x1a1c4, 0x1a1c2, 0x143d0, 0x1a1ec, 0x143c8, 0x1a1e6, 0x143c4, 0x143c2, 0x143ec, 0x143e6, 0x1e828, 0x1f416, 0x1e824, 0x1e822, 
            0x1d068, 0x1e836, 0x1d064, 0x1d062, 0x1a0e8, 0x1d076, 0x1a0e4, 0x1a0e2, 0x141e8, 0x1a0f6, 0x141e4, 0x141e2, 0x1e814, 0x1e812, 0x1d034, 0x1d032, 
            0x1a074, 0x1a072, 0x1e540, 0x1f2b0, 0x1f95c, 0x1e520, 0x1f298, 0x1f94e, 0x1e510, 0x1f28c, 0x1e508, 0x1f286, 0x1e504, 0x1e502, 0x1cb40, 0x1e5b0, 
            0x1f2dc, 0x1cb20, 0x1e598, 0x1f2ce, 0x1cb10, 0x1e58c, 0x1cb08, 0x1e586, 0x1cb04, 0x1cb02, 0x19740, 0x1cbb0, 0x1e5dc, 0x19720, 0x1cb98, 0x1e5ce, 
            0x19710, 0x1cb8c, 0x19708, 0x1cb86, 0x19704, 0x19702, 0x12f40, 0x197b0, 0x1cbdc, 0x12f20, 0x19798, 0x1cbce, 0x12f10, 0x1978c, 0x12f08, 0x19786, 
            0x12f04, 0x12fb0, 0x197dc, 0x12f98, 0x197ce, 0x12f8c, 0x12f86, 0x12fdc, 0x12fce, 0x1f6a0, 0x1fb58, 0x16bf0, 0x1f690, 0x1fb4c, 0x169f8, 0x1f688, 
            0x1fb46, 0x168fc, 0x1f684, 0x1f682, 0x1e4a0, 0x1f258, 0x1f92e, 0x1eda0, 0x1e490, 0x1fb6e, 0x1ed90, 0x1f6cc, 0x1f246, 0x1ed88, 0x1e484, 0x1ed84, 
            0x1e482, 0x1ed82, 0x1c9a0, 0x1e4d8, 0x1f26e, 0x1dba0, 0x1c990, 0x1e4cc, 0x1db90, 0x1edcc, 0x1e4c6, 0x1db88, 0x1c984, 0x1db84, 0x1c982, 0x1db82, 
            0x193a0, 0x1c9d8, 0x1e4ee, 0x1b7a0, 0x19390, 0x1c9cc, 0x1b790, 0x1dbcc, 0x1c9c6, 0x1b788, 0x19384, 0x1b784, 0x19382, 0x1b782, 0x127a0, 0x193d8, 
            0x1c9ee, 0x16fa0, 0x12790, 0x193cc, 0x16f90, 0x1b7cc, 0x193c6, 0x16f88, 0x12784, 0x16f84, 0x12782, 0x127d8, 0x193ee, 0x16fd8, 0x127cc, 0x16fcc, 
            0x127c6, 0x16fc6, 0x127ee, 0x1f650, 0x1fb2c, 0x165f8, 0x1f648, 0x1fb26, 0x164fc, 0x1f644, 0x1647e, 0x1f642, 0x1e450, 0x1f22c, 0x1ecd0, 0x1e448, 
            0x1f226, 0x1ecc8, 0x1f666, 0x1ecc4, 0x1e442, 0x1ecc2, 0x1c8d0, 0x1e46c, 0x1d9d0, 0x1c8c8, 0x1e466, 0x1d9c8, 0x1ece6, 0x1d9c4, 0x1c8c2, 0x1d9c2, 
            0x191d0, 0x1c8ec, 0x1b3d0, 0x191c8, 0x1c8e6, 0x1b3c8, 0x1d9e6, 0x1b3c4, 0x191c2, 0x1b3c2, 0x123d0, 0x191ec, 0x167d0, 0x123c8, 0x191e6, 0x167c8, 
            0x1b3e6, 0x167c4, 0x123c2, 0x167c2, 0x123ec, 0x167ec, 0x123e6, 0x167e6, 0x1f628, 0x1fb16, 0x162fc, 0x1f624, 0x1627e, 0x1f622, 0x1e428, 0x1f216, 
            0x1ec68, 0x1f636, 0x1ec64, 0x1e422, 0x1ec62, 0x1c868, 0x1e436, 0x1d8e8, 0x1c864, 0x1d8e4, 0x1c862, 0x1d8e2, 0x190e8, 0x1c876, 0x1b1e8, 0x1d8f6, 
            0x1b1e4, 0x190e2, 0x1b1e2, 0x121e8, 0x190f6, 0x163e8, 0x121e4, 0x163e4, 0x121e2, 0x163e2, 0x121f6, 0x163f6, 0x1f614, 0x1617e, 0x1f612, 0x1e414, 
            0x1ec34, 0x1e412, 0x1ec32, 0x1c834, 0x1d874, 0x1c832, 0x1d872, 0x19074, 0x1b0f4, 0x19072, 0x1b0f2, 0x120f4, 0x161f4, 0x120f2, 0x161f2, 0x1f60a, 
            0x1e40a, 0x1ec1a, 0x1c81a, 0x1d83a, 0x1903a, 0x1b07a, 0x1e2a0, 0x1f158, 0x1f8ae, 0x1e290, 0x1f14c, 0x1e288, 0x1f146, 0x1e284, 0x1e282, 0x1c5a0, 
            0x1e2d8, 0x1f16e, 0x1c590, 0x1e2cc, 0x1c588, 0x1e2c6, 0x1c584, 0x1c582, 0x18ba0, 0x1c5d8, 0x1e2ee, 0x18b90, 0x1c5cc, 0x18b88, 0x1c5c6, 0x18b84, 
            0x18b82, 0x117a0, 0x18bd8, 0x1c5ee, 0x11790, 0x18bcc, 0x11788, 0x18bc6, 0x11784, 0x11782, 0x117d8, 0x18bee, 0x117cc, 0x117c6, 0x117ee, 0x1f350, 
            0x1f9ac, 0x135f8, 0x1f348, 0x1f9a6, 0x134fc, 0x1f344, 0x1347e, 0x1f342, 0x1e250, 0x1f12c, 0x1e6d0, 0x1e248, 0x1f126, 0x1e6c8, 0x1f366, 0x1e6c4, 
            0x1e242, 0x1e6c2, 0x1c4d0, 0x1e26c, 0x1cdd0, 0x1c4c8, 0x1e266, 0x1cdc8, 0x1e6e6, 0x1cdc4, 0x1c4c2, 0x1cdc2, 0x189d0, 0x1c4ec, 0x19bd0, 0x189c8, 
            0x1c4e6, 0x19bc8, 0x1cde6, 0x19bc4, 0x189c2, 0x19bc2, 0x113d0, 0x189ec, 0x137d0, 0x113c8, 0x189e6, 0x137c8, 0x19be6, 0x137c4, 0x113c2, 0x137c2, 
            0x113ec, 0x137ec, 0x113e6, 0x137e6, 0x1fba8, 0x175f0, 0x1bafc, 0x1fba4, 0x174f8, 0x1ba7e, 0x1fba2, 0x1747c, 0x1743e, 0x1f328, 0x1f996, 0x132fc, 
            0x1f768, 0x1fbb6, 0x176fc, 0x1327e, 0x1f764, 0x1f322, 0x1767e, 0x1f762, 0x1e228, 0x1f116, 0x1e668, 0x1e224, 0x1eee8, 0x1f776, 0x1e222, 0x1eee4, 
            0x1e662, 0x1eee2, 0x1c468, 0x1e236, 0x1cce8, 0x1c464, 0x1dde8, 0x1cce4, 0x1c462, 0x1dde4, 0x1cce2, 0x1dde2, 0x188e8, 0x1c476, 0x199e8, 0x188e4, 
            0x1bbe8, 0x199e4, 0x188e2, 0x1bbe4, 0x199e2, 0x1bbe2, 0x111e8, 0x188f6, 0x133e8, 0x111e4, 0x177e8, 0x133e4, 0x111e2, 0x177e4, 0x133e2, 0x177e2, 
            0x111f6, 0x133f6, 0x1fb94, 0x172f8, 0x1b97e, 0x1fb92, 0x1727c, 0x1723e, 0x1f314, 0x1317e, 0x1f734, 0x1f312, 0x1737e, 0x1f732, 0x1e214, 0x1e634, 
            0x1e212, 0x1ee74, 0x1e632, 0x1ee72, 0x1c434, 0x1cc74, 0x1c432, 0x1dcf4, 0x1cc72, 0x1dcf2, 0x18874, 0x198f4, 0x18872, 0x1b9f4, 0x198f2, 0x1b9f2, 
            0x110f4, 0x131f4, 0x110f2, 0x173f4, 0x131f2, 0x173f2, 0x1fb8a, 0x1717c, 0x1713e, 0x1f30a, 0x1f71a, 0x1e20a, 0x1e61a, 0x1ee3a, 0x1c41a, 0x1cc3a, 
            0x1dc7a, 0x1883a, 0x1987a, 0x1b8fa, 0x1107a, 0x130fa, 0x171fa, 0x170be, 0x1e150, 0x1f0ac, 0x1e148, 0x1f0a6, 0x1e144, 0x1e142, 0x1c2d0, 0x1e16c, 
            0x1c2c8, 0x1e166, 0x1c2c4, 0x1c2c2, 0x185d0, 0x1c2ec, 0x185c8, 0x1c2e6, 0x185c4, 0x185c2, 0x10bd0, 0x185ec, 0x10bc8, 0x185e6, 0x10bc4, 0x10bc2, 
            0x10bec, 0x10be6, 0x1f1a8, 0x1f8d6, 0x11afc, 0x1f1a4, 0x11a7e, 0x1f1a2, 0x1e128, 0x1f096, 0x1e368, 0x1e124, 0x1e364, 0x1e122, 0x1e362, 0x1c268, 
            0x1e136, 0x1c6e8, 0x1c264, 0x1c6e4, 0x1c262, 0x1c6e2, 0x184e8, 0x1c276, 0x18de8, 0x184e4, 0x18de4, 0x184e2, 0x18de2, 0x109e8, 0x184f6, 0x11be8, 
            0x109e4, 0x11be4, 0x109e2, 0x11be2, 0x109f6, 0x11bf6, 0x1f9d4, 0x13af8, 0x19d7e, 0x1f9d2, 0x13a7c, 0x13a3e, 0x1f194, 0x1197e, 0x1f3b4, 0x1f192, 
            0x13b7e, 0x1f3b2, 0x1e114, 0x1e334, 0x1e112, 0x1e774, 0x1e332, 0x1e772, 0x1c234, 0x1c674, 0x1c232, 0x1cef4, 0x1c672, 0x1cef2, 0x18474, 0x18cf4, 
            0x18472, 0x19df4, 0x18cf2, 0x19df2, 0x108f4, 0x119f4, 0x108f2, 0x13bf4, 0x119f2, 0x13bf2, 0x17af0, 0x1bd7c, 0x17a78, 0x1bd3e, 0x17a3c, 0x17a1e, 
            0x1f9ca, 0x1397c, 0x1fbda, 0x17b7c, 0x1393e, 0x17b3e, 0x1f18a, 0x1f39a, 0x1f7ba, 0x1e10a, 0x1e31a, 0x1e73a, 0x1ef7a, 0x1c21a, 0x1c63a, 0x1ce7a, 
            0x1defa, 0x1843a, 0x18c7a, 0x19cfa, 0x1bdfa, 0x1087a, 0x118fa, 0x139fa, 0x17978, 0x1bcbe, 0x1793c, 0x1791e, 0x138be, 0x179be, 0x178bc, 0x1789e, 
            0x1785e, 0x1e0a8, 0x1e0a4, 0x1e0a2, 0x1c168, 0x1e0b6, 0x1c164, 0x1c162, 0x182e8, 0x1c176, 0x182e4, 0x182e2, 0x105e8, 0x182f6, 0x105e4, 0x105e2, 
            0x105f6, 0x1f0d4, 0x10d7e, 0x1f0d2, 0x1e094, 0x1e1b4, 0x1e092, 0x1e1b2, 0x1c134, 0x1c374, 0x1c132, 0x1c372, 0x18274, 0x186f4, 0x18272, 0x186f2, 
            0x104f4, 0x10df4, 0x104f2, 0x10df2, 0x1f8ea, 0x11d7c, 0x11d3e, 0x1f0ca, 0x1f1da, 0x1e08a, 0x1e19a, 0x1e3ba, 0x1c11a, 0x1c33a, 0x1c77a, 0x1823a, 
            0x1867a, 0x18efa, 0x1047a, 0x10cfa, 0x11dfa, 0x13d78, 0x19ebe, 0x13d3c, 0x13d1e, 0x11cbe, 0x13dbe, 0x17d70, 0x1bebc, 0x17d38, 0x1be9e, 0x17d1c, 
            0x17d0e, 0x13cbc, 0x17dbc, 0x13c9e, 0x17d9e, 0x17cb8, 0x1be5e, 0x17c9c, 0x17c8e, 0x13c5e, 0x17cde, 0x17c5c, 0x17c4e, 0x17c2e, 0x1c0b4, 0x1c0b2, 
            0x18174, 0x18172, 0x102f4, 0x102f2, 0x1e0da, 0x1c09a, 0x1c1ba, 0x1813a, 0x1837a, 0x1027a, 0x106fa, 0x10ebe, 0x11ebc, 0x11e9e, 0x13eb8, 0x19f5e, 
            0x13e9c, 0x13e8e, 0x11e5e, 0x13ede, 0x17eb0, 0x1bf5c, 0x17e98, 0x1bf4e, 0x17e8c, 0x17e86, 0x13e5c, 0x17edc, 0x13e4e, 0x17ece, 0x17e58, 0x1bf2e, 
            0x17e4c, 0x17e46, 0x13e2e, 0x17e6e, 0x17e2c, 0x17e26, 0x10f5e, 0x11f5c, 0x11f4e, 0x13f58, 0x19fae, 0x13f4c, 0x13f46, 0x11f2e, 0x13f6e, 0x13f2c, 
            0x13f26
         }, new int[] { 
            0x1abe0, 0x1d5f8, 0x153c0, 0x1a9f0, 0x1d4fc, 0x151e0, 0x1a8f8, 0x1d47e, 0x150f0, 0x1a87c, 0x15078, 0x1fad0, 0x15be0, 0x1adf8, 0x1fac8, 0x159f0, 
            0x1acfc, 0x1fac4, 0x158f8, 0x1ac7e, 0x1fac2, 0x1587c, 0x1f5d0, 0x1faec, 0x15df8, 0x1f5c8, 0x1fae6, 0x15cfc, 0x1f5c4, 0x15c7e, 0x1f5c2, 0x1ebd0, 
            0x1f5ec, 0x1ebc8, 0x1f5e6, 0x1ebc4, 0x1ebc2, 0x1d7d0, 0x1ebec, 0x1d7c8, 0x1ebe6, 0x1d7c4, 0x1d7c2, 0x1afd0, 0x1d7ec, 0x1afc8, 0x1d7e6, 0x1afc4, 
            0x14bc0, 0x1a5f0, 0x1d2fc, 0x149e0, 0x1a4f8, 0x1d27e, 0x148f0, 0x1a47c, 0x14878, 0x1a43e, 0x1483c, 0x1fa68, 0x14df0, 0x1a6fc, 0x1fa64, 0x14cf8, 
            0x1a67e, 0x1fa62, 0x14c7c, 0x14c3e, 0x1f4e8, 0x1fa76, 0x14efc, 0x1f4e4, 0x14e7e, 0x1f4e2, 0x1e9e8, 0x1f4f6, 0x1e9e4, 0x1e9e2, 0x1d3e8, 0x1e9f6, 
            0x1d3e4, 0x1d3e2, 0x1a7e8, 0x1d3f6, 0x1a7e4, 0x1a7e2, 0x145e0, 0x1a2f8, 0x1d17e, 0x144f0, 0x1a27c, 0x14478, 0x1a23e, 0x1443c, 0x1441e, 0x1fa34, 
            0x146f8, 0x1a37e, 0x1fa32, 0x1467c, 0x1463e, 0x1f474, 0x1477e, 0x1f472, 0x1e8f4, 0x1e8f2, 0x1d1f4, 0x1d1f2, 0x1a3f4, 0x1a3f2, 0x142f0, 0x1a17c, 
            0x14278, 0x1a13e, 0x1423c, 0x1421e, 0x1fa1a, 0x1437c, 0x1433e, 0x1f43a, 0x1e87a, 0x1d0fa, 0x14178, 0x1a0be, 0x1413c, 0x1411e, 0x141be, 0x140bc, 
            0x1409e, 0x12bc0, 0x195f0, 0x1cafc, 0x129e0, 0x194f8, 0x1ca7e, 0x128f0, 0x1947c, 0x12878, 0x1943e, 0x1283c, 0x1f968, 0x12df0, 0x196fc, 0x1f964, 
            0x12cf8, 0x1967e, 0x1f962, 0x12c7c, 0x12c3e, 0x1f2e8, 0x1f976, 0x12efc, 0x1f2e4, 0x12e7e, 0x1f2e2, 0x1e5e8, 0x1f2f6, 0x1e5e4, 0x1e5e2, 0x1cbe8, 
            0x1e5f6, 0x1cbe4, 0x1cbe2, 0x197e8, 0x1cbf6, 0x197e4, 0x197e2, 0x1b5e0, 0x1daf8, 0x1ed7e, 0x169c0, 0x1b4f0, 0x1da7c, 0x168e0, 0x1b478, 0x1da3e, 
            0x16870, 0x1b43c, 0x16838, 0x1b41e, 0x1681c, 0x125e0, 0x192f8, 0x1c97e, 0x16de0, 0x124f0, 0x1927c, 0x16cf0, 0x1b67c, 0x1923e, 0x16c78, 0x1243c, 
            0x16c3c, 0x1241e, 0x16c1e, 0x1f934, 0x126f8, 0x1937e, 0x1fb74, 0x1f932, 0x16ef8, 0x1267c, 0x1fb72, 0x16e7c, 0x1263e, 0x16e3e, 0x1f274, 0x1277e, 
            0x1f6f4, 0x1f272, 0x16f7e, 0x1f6f2, 0x1e4f4, 0x1edf4, 0x1e4f2, 0x1edf2, 0x1c9f4, 0x1dbf4, 0x1c9f2, 0x1dbf2, 0x193f4, 0x193f2, 0x165c0, 0x1b2f0, 
            0x1d97c, 0x164e0, 0x1b278, 0x1d93e, 0x16470, 0x1b23c, 0x16438, 0x1b21e, 0x1641c, 0x1640e, 0x122f0, 0x1917c, 0x166f0, 0x12278, 0x1913e, 0x16678, 
            0x1b33e, 0x1663c, 0x1221e, 0x1661e, 0x1f91a, 0x1237c, 0x1fb3a, 0x1677c, 0x1233e, 0x1673e, 0x1f23a, 0x1f67a, 0x1e47a, 0x1ecfa, 0x1c8fa, 0x1d9fa, 
            0x191fa, 0x162e0, 0x1b178, 0x1d8be, 0x16270, 0x1b13c, 0x16238, 0x1b11e, 0x1621c, 0x1620e, 0x12178, 0x190be, 0x16378, 0x1213c, 0x1633c, 0x1211e, 
            0x1631e, 0x121be, 0x163be, 0x16170, 0x1b0bc, 0x16138, 0x1b09e, 0x1611c, 0x1610e, 0x120bc, 0x161bc, 0x1209e, 0x1619e, 0x160b8, 0x1b05e, 0x1609c, 
            0x1608e, 0x1205e, 0x160de, 0x1605c, 0x1604e, 0x115e0, 0x18af8, 0x1c57e, 0x114f0, 0x18a7c, 0x11478, 0x18a3e, 0x1143c, 0x1141e, 0x1f8b4, 0x116f8, 
            0x18b7e, 0x1f8b2, 0x1167c, 0x1163e, 0x1f174, 0x1177e, 0x1f172, 0x1e2f4, 0x1e2f2, 0x1c5f4, 0x1c5f2, 0x18bf4, 0x18bf2, 0x135c0, 0x19af0, 0x1cd7c, 
            0x134e0, 0x19a78, 0x1cd3e, 0x13470, 0x19a3c, 0x13438, 0x19a1e, 0x1341c, 0x1340e, 0x112f0, 0x1897c, 0x136f0, 0x11278, 0x1893e, 0x13678, 0x19b3e, 
            0x1363c, 0x1121e, 0x1361e, 0x1f89a, 0x1137c, 0x1f9ba, 0x1377c, 0x1133e, 0x1373e, 0x1f13a, 0x1f37a, 0x1e27a, 0x1e6fa, 0x1c4fa, 0x1cdfa, 0x189fa, 
            0x1bae0, 0x1dd78, 0x1eebe, 0x174c0, 0x1ba70, 0x1dd3c, 0x17460, 0x1ba38, 0x1dd1e, 0x17430, 0x1ba1c, 0x17418, 0x1ba0e, 0x1740c, 0x132e0, 0x19978, 
            0x1ccbe, 0x176e0, 0x13270, 0x1993c, 0x17670, 0x1bb3c, 0x1991e, 0x17638, 0x1321c, 0x1761c, 0x1320e, 0x1760e, 0x11178, 0x188be, 0x13378, 0x1113c, 
            0x17778, 0x1333c, 0x1111e, 0x1773c, 0x1331e, 0x1771e, 0x111be, 0x133be, 0x177be, 0x172c0, 0x1b970, 0x1dcbc, 0x17260, 0x1b938, 0x1dc9e, 0x17230, 
            0x1b91c, 0x17218, 0x1b90e, 0x1720c, 0x17206, 0x13170, 0x198bc, 0x17370, 0x13138, 0x1989e, 0x17338, 0x1b99e, 0x1731c, 0x1310e, 0x1730e, 0x110bc, 
            0x131bc, 0x1109e, 0x173bc, 0x1319e, 0x1739e, 0x17160, 0x1b8b8, 0x1dc5e, 0x17130, 0x1b89c, 0x17118, 0x1b88e, 0x1710c, 0x17106, 0x130b8, 0x1985e, 
            0x171b8, 0x1309c, 0x1719c, 0x1308e, 0x1718e, 0x1105e, 0x130de, 0x171de, 0x170b0, 0x1b85c, 0x17098, 0x1b84e, 0x1708c, 0x17086, 0x1305c, 0x170dc, 
            0x1304e, 0x170ce, 0x17058, 0x1b82e, 0x1704c, 0x17046, 0x1302e, 0x1706e, 0x1702c, 0x17026, 0x10af0, 0x1857c, 0x10a78, 0x1853e, 0x10a3c, 0x10a1e, 
            0x10b7c, 0x10b3e, 0x1f0ba, 0x1e17a, 0x1c2fa, 0x185fa, 0x11ae0, 0x18d78, 0x1c6be, 0x11a70, 0x18d3c, 0x11a38, 0x18d1e, 0x11a1c, 0x11a0e, 0x10978, 
            0x184be, 0x11b78, 0x1093c, 0x11b3c, 0x1091e, 0x11b1e, 0x109be, 0x11bbe, 0x13ac0, 0x19d70, 0x1cebc, 0x13a60, 0x19d38, 0x1ce9e, 0x13a30, 0x19d1c, 
            0x13a18, 0x19d0e, 0x13a0c, 0x13a06, 0x11970, 0x18cbc, 0x13b70, 0x11938, 0x18c9e, 0x13b38, 0x1191c, 0x13b1c, 0x1190e, 0x13b0e, 0x108bc, 0x119bc, 
            0x1089e, 0x13bbc, 0x1199e, 0x13b9e, 0x1bd60, 0x1deb8, 0x1ef5e, 0x17a40, 0x1bd30, 0x1de9c, 0x17a20, 0x1bd18, 0x1de8e, 0x17a10, 0x1bd0c, 0x17a08, 
            0x1bd06, 0x17a04, 0x13960, 0x19cb8, 0x1ce5e, 0x17b60, 0x13930, 0x19c9c, 0x17b30, 0x1bd9c, 0x19c8e, 0x17b18, 0x1390c, 0x17b0c, 0x13906, 0x17b06, 
            0x118b8, 0x18c5e, 0x139b8, 0x1189c, 0x17bb8, 0x1399c, 0x1188e, 0x17b9c, 0x1398e, 0x17b8e, 0x1085e, 0x118de, 0x139de, 0x17bde, 0x17940, 0x1bcb0, 
            0x1de5c, 0x17920, 0x1bc98, 0x1de4e, 0x17910, 0x1bc8c, 0x17908, 0x1bc86, 0x17904, 0x17902, 0x138b0, 0x19c5c, 0x179b0, 0x13898, 0x19c4e, 0x17998, 
            0x1bcce, 0x1798c, 0x13886, 0x17986, 0x1185c, 0x138dc, 0x1184e, 0x179dc, 0x138ce, 0x179ce, 0x178a0, 0x1bc58, 0x1de2e, 0x17890, 0x1bc4c, 0x17888, 
            0x1bc46, 0x17884, 0x17882, 0x13858, 0x19c2e, 0x178d8, 0x1384c, 0x178cc, 0x13846, 0x178c6, 0x1182e, 0x1386e, 0x178ee, 0x17850, 0x1bc2c, 0x17848, 
            0x1bc26, 0x17844, 0x17842, 0x1382c, 0x1786c, 0x13826, 0x17866, 0x17828, 0x1bc16, 0x17824, 0x17822, 0x13816, 0x17836, 0x10578, 0x182be, 0x1053c, 
            0x1051e, 0x105be, 0x10d70, 0x186bc, 0x10d38, 0x1869e, 0x10d1c, 0x10d0e, 0x104bc, 0x10dbc, 0x1049e, 0x10d9e, 0x11d60, 0x18eb8, 0x1c75e, 0x11d30, 
            0x18e9c, 0x11d18, 0x18e8e, 0x11d0c, 0x11d06, 0x10cb8, 0x1865e, 0x11db8, 0x10c9c, 0x11d9c, 0x10c8e, 0x11d8e, 0x1045e, 0x10cde, 0x11dde, 0x13d40, 
            0x19eb0, 0x1cf5c, 0x13d20, 0x19e98, 0x1cf4e, 0x13d10, 0x19e8c, 0x13d08, 0x19e86, 0x13d04, 0x13d02, 0x11cb0, 0x18e5c, 0x13db0, 0x11c98, 0x18e4e, 
            0x13d98, 0x19ece, 0x13d8c, 0x11c86, 0x13d86, 0x10c5c, 0x11cdc, 0x10c4e, 0x13ddc, 0x11cce, 0x13dce, 0x1bea0, 0x1df58, 0x1efae, 0x1be90, 0x1df4c, 
            0x1be88, 0x1df46, 0x1be84, 0x1be82, 0x13ca0, 0x19e58, 0x1cf2e, 0x17da0, 0x13c90, 0x19e4c, 0x17d90, 0x1becc, 0x19e46, 0x17d88, 0x13c84, 0x17d84, 
            0x13c82, 0x17d82, 0x11c58, 0x18e2e, 0x13cd8, 0x11c4c, 0x17dd8, 0x13ccc, 0x11c46, 0x17dcc, 0x13cc6, 0x17dc6, 0x10c2e, 0x11c6e, 0x13cee, 0x17dee, 
            0x1be50, 0x1df2c, 0x1be48, 0x1df26, 0x1be44, 0x1be42, 0x13c50, 0x19e2c, 0x17cd0, 0x13c48, 0x19e26, 0x17cc8, 0x1be66, 0x17cc4, 0x13c42, 0x17cc2, 
            0x11c2c, 0x13c6c, 0x11c26, 0x17cec, 0x13c66, 0x17ce6, 0x1be28, 0x1df16, 0x1be24, 0x1be22, 0x13c28, 0x19e16, 0x17c68, 0x13c24, 0x17c64, 0x13c22, 
            0x17c62, 0x11c16, 0x13c36, 0x17c76, 0x1be14, 0x1be12, 0x13c14, 0x17c34, 0x13c12, 0x17c32, 0x102bc, 0x1029e, 0x106b8, 0x1835e, 0x1069c, 0x1068e, 
            0x1025e, 0x106de, 0x10eb0, 0x1875c, 0x10e98, 0x1874e, 0x10e8c, 0x10e86, 0x1065c, 0x10edc, 0x1064e, 0x10ece, 0x11ea0, 0x18f58, 0x1c7ae, 0x11e90, 
            0x18f4c, 0x11e88, 0x18f46, 0x11e84, 0x11e82, 0x10e58, 0x1872e, 0x11ed8, 0x18f6e, 0x11ecc, 0x10e46, 0x11ec6, 0x1062e, 0x10e6e, 0x11eee, 0x19f50, 
            0x1cfac, 0x19f48, 0x1cfa6, 0x19f44, 0x19f42, 0x11e50, 0x18f2c, 0x13ed0, 0x19f6c, 0x18f26, 0x13ec8, 0x11e44, 0x13ec4, 0x11e42, 0x13ec2, 0x10e2c, 
            0x11e6c, 0x10e26, 0x13eec, 0x11e66, 0x13ee6, 0x1dfa8, 0x1efd6, 0x1dfa4, 0x1dfa2, 0x19f28, 0x1cf96, 0x1bf68, 0x19f24, 0x1bf64, 0x19f22, 0x1bf62, 
            0x11e28, 0x18f16, 0x13e68, 0x11e24, 0x17ee8, 0x13e64, 0x11e22, 0x17ee4, 0x13e62, 0x17ee2, 0x10e16, 0x11e36, 0x13e76, 0x17ef6, 0x1df94, 0x1df92, 
            0x19f14, 0x1bf34, 0x19f12, 0x1bf32, 0x11e14, 0x13e34, 0x11e12, 0x17e74, 0x13e32, 0x17e72, 0x1df8a, 0x19f0a, 0x1bf1a, 0x11e0a, 0x13e1a, 0x17e3a, 
            0x1035c, 0x1034e, 0x10758, 0x183ae, 0x1074c, 0x10746, 0x1032e, 0x1076e, 0x10f50, 0x187ac, 0x10f48, 0x187a6, 0x10f44, 0x10f42, 0x1072c, 0x10f6c, 
            0x10726, 0x10f66, 0x18fa8, 0x1c7d6, 0x18fa4, 0x18fa2, 0x10f28, 0x18796, 0x11f68, 0x18fb6, 0x11f64, 0x10f22, 0x11f62, 0x10716, 0x10f36, 0x11f76, 
            0x1cfd4, 0x1cfd2, 0x18f94, 0x19fb4, 0x18f92, 0x19fb2, 0x10f14, 0x11f34, 0x10f12, 0x13f74, 0x11f32, 0x13f72, 0x1cfca, 0x18f8a, 0x19f9a, 0x10f0a, 
            0x11f1a, 0x13f3a, 0x103ac, 0x103a6, 0x107a8, 0x183d6, 0x107a4, 0x107a2, 0x10396, 0x107b6, 0x187d4, 0x187d2, 0x10794, 0x10fb4, 0x10792, 0x10fb2, 
            0x1c7ea
         } };
        public int codeColumns = -1;
        public int codeRows = -1;
        private int codewordPtr = -1;
        private int[] codewords = new int[0x3a0];
        private const int COMPACT_AUTO = 0;
        private const int COMPACT_BINARY = 3;
        private const int COMPACT_NUMBER = 2;
        private const int COMPACT_TEXT = 1;
        private int compactMode;
        public string data = "BarCodeImage PDF417 demo";
        private int dataLen;
        private const float DEFAULT_ASPECTRATIO = 0.5f;
        private const int DEFAULT_FIXEDCOLUMNS = 3;
        private const int DEFAULT_FIXEDROWS = 3;
        private const bool DEFAULT_ISTRUNCATED = false;
        private const bool DETAULT_ISTRUNCATED = false;
        private int error;
        private static int[] ERROR_LEVEL0 = new int[] { 0x1b, 0x395 };
        private static int[] ERROR_LEVEL1 = new int[] { 0x20a, 0x238, 0x2d3, 0x329 };
        private static int[] ERROR_LEVEL2 = new int[] { 0xed, 0x134, 0x1b4, 0x11c, 0x286, 0x28d, 0x1ac, 0x17b };
        private static int[] ERROR_LEVEL3 = new int[] { 0x112, 0x232, 0xe8, 0x2f3, 0x257, 0x20c, 0x321, 0x84, 0x127, 0x74, 0x1ba, 0x1ac, 0x127, 0x2a, 0xb0, 0x41 };
        private static int[] ERROR_LEVEL4 = new int[] { 
            0x169, 0x23f, 0x39a, 0x20d, 0xb0, 0x24a, 640, 0x141, 0x218, 0x2e6, 0x2a5, 0x2e6, 0x2af, 0x11c, 0xc1, 0x205, 
            0x111, 0x1ee, 0x107, 0x93, 0x251, 800, 0x23b, 320, 0x323, 0x85, 0xe7, 390, 0x2ad, 330, 0x3f, 410
         };
        private static int[] ERROR_LEVEL5 = new int[] { 
            0x21b, 0x1a6, 6, 0x5d, 0x35e, 0x303, 0x1c5, 0x6a, 610, 0x11f, 0x6b, 0x1f9, 0x2dd, 0x36d, 0x17d, 0x264, 
            0x2d3, 0x1dc, 0x1ce, 0xac, 430, 0x261, 0x35a, 0x336, 0x21f, 0x178, 0x1ff, 400, 0x2a0, 0x2fa, 0x11b, 0xb8, 
            440, 0x23, 0x207, 0x1f, 460, 0x252, 0xe1, 0x217, 0x205, 0x160, 0x25d, 0x9e, 0x28b, 0xc9, 0x1e8, 0x1f6, 
            0x288, 0x2dd, 0x2cd, 0x53, 0x194, 0x61, 280, 0x303, 840, 0x275, 4, 0x17d, 0x34b, 0x26f, 0x108, 0x21f
         };
        private static int[] ERROR_LEVEL6 = new int[] { 
            0x209, 310, 0x360, 0x223, 0x35a, 580, 0x128, 0x17b, 0x35, 0x30b, 0x381, 0x1bc, 400, 0x39d, 0x2ed, 0x19f, 
            0x336, 0x5d, 0xd9, 0xd0, 0x3a0, 0xf4, 0x247, 620, 0xf6, 0x94, 0x1bf, 0x277, 0x124, 0x38c, 490, 0x2c0, 
            0x204, 0x102, 0x1c9, 0x38b, 0x252, 0x2d3, 0x2a2, 0x124, 0x110, 0x60, 0x2ac, 0x1b0, 0x2ae, 0x25e, 860, 0x239, 
            0xc1, 0xdb, 0x81, 0xba, 0xec, 0x11f, 0xc0, 0x307, 0x116, 0xad, 40, 0x17b, 0x2c8, 0x1cf, 0x286, 0x308, 
            0xab, 0x1eb, 0x129, 0x2fb, 0x9c, 0x2dc, 0x5f, 270, 0x1bf, 90, 0x1fb, 0x30, 0xe4, 0x335, 0x328, 0x382, 
            0x310, 0x297, 0x273, 0x17a, 0x17e, 0x106, 380, 0x25a, 0x2f2, 0x150, 0x59, 0x266, 0x57, 0x1b0, 670, 0x268, 
            0x9d, 0x176, 0xf2, 0x2d6, 600, 0x10d, 0x177, 0x382, 0x34d, 0x1c6, 0x162, 130, 0x32e, 0x24b, 0x324, 0x22, 
            0xd3, 330, 0x21b, 0x129, 0x33b, 0x361, 0x25, 0x205, 0x342, 0x13b, 550, 0x56, 0x321, 4, 0x6c, 0x21b
         };
        private static int[] ERROR_LEVEL7 = new int[] { 
            0x20c, 0x37e, 0x4b, 0x2fe, 0x372, 0x359, 0x4a, 0xcc, 0x52, 0x24a, 0x2c4, 250, 0x389, 0x312, 0x8a, 720, 
            0x35a, 0xc2, 0x137, 0x391, 0x113, 190, 0x177, 850, 0x1b6, 0x2dd, 0xc2, 280, 0xc9, 280, 0x33c, 0x2f5, 
            710, 0x32e, 0x397, 0x59, 0x44, 0x239, 11, 0xcc, 0x31c, 0x25d, 540, 0x391, 0x321, 700, 0x31f, 0x89, 
            0x1b7, 0x1a2, 0x250, 0x29c, 0x161, 0x35b, 370, 0x2b6, 0x145, 240, 0xd8, 0x101, 0x11c, 0x225, 0xd1, 0x374, 
            0x13b, 70, 0x149, 0x319, 490, 0x112, 0x36d, 0xa2, 0x2ed, 0x32c, 0x2ac, 0x1cd, 0x14e, 0x178, 0x351, 0x209, 
            0x133, 0x123, 0x323, 0x2c8, 0x13, 0x166, 0x18f, 0x38c, 0x67, 0x1ff, 0x33, 8, 0x205, 0xe1, 0x121, 470, 
            0x27d, 0x2db, 0x42, 0xff, 0x395, 0x10d, 0x1cf, 830, 730, 0x1b1, 0x350, 0x249, 0x88, 0x21a, 0x38a, 90, 
            2, 290, 0x2e7, 0xc7, 0x28f, 0x387, 0x149, 0x31, 0x322, 580, 0x163, 0x24c, 0xbc, 0x1ce, 10, 0x86, 
            0x274, 320, 0x1df, 130, 0x2e3, 0x47, 0x107, 0x13e, 0x176, 0x259, 0xc0, 0x25d, 0x8e, 0x2a1, 0x2af, 0xea, 
            0x2d2, 0x180, 0xb1, 0x2f0, 0x25f, 640, 0x1c7, 0xc1, 0x2b1, 0x2c3, 0x325, 0x281, 0x30, 60, 0x2dc, 0x26d, 
            0x37f, 0x220, 0x105, 0x354, 0x28f, 0x135, 0x2b9, 0x2f3, 0x2f4, 60, 0xe7, 0x305, 0x1b2, 0x1a5, 0x2d6, 0x210, 
            0x1f7, 0x76, 0x31, 0x31b, 0x20, 0x90, 500, 0xee, 0x344, 0x18a, 280, 0x236, 0x13f, 9, 0x287, 550, 
            0x49, 0x392, 0x156, 0x7e, 0x20, 0x2a9, 0x14b, 0x318, 620, 60, 0x261, 0x1b9, 180, 0x317, 0x37d, 0x2f2, 
            0x25d, 0x17f, 0xe4, 0x2ed, 760, 0xd5, 0x36, 0x129, 0x86, 0x36, 0x342, 0x12b, 0x39a, 0xbf, 910, 0x214, 
            0x261, 0x33d, 0xbd, 20, 0xa7, 0x1d, 0x368, 0x1c1, 0x53, 0x192, 0x29, 0x290, 0x1f9, 0x243, 0x1e1, 0xad, 
            0x194, 0xfb, 0x2b0, 0x5f, 0x1f1, 0x22b, 0x282, 0x21f, 0x133, 0x9f, 0x39c, 0x22e, 0x288, 0x37, 0x1f1, 10
         };
        private static int[] ERROR_LEVEL8 = new int[] { 
            0x160, 0x4d, 0x175, 0x1f8, 0x23, 0x257, 0x1ac, 0xcf, 0x199, 0x23e, 0x76, 0x1f2, 0x11d, 380, 350, 0x1ec, 
            0xc5, 0x109, 920, 0x9b, 0x392, 0x12b, 0xe5, 0x283, 0x126, 0x367, 0x132, 0x58, 0x57, 0xc1, 0x160, 0x30d, 
            0x34e, 0x4b, 0x147, 520, 0x1b3, 0x21f, 0xcb, 0x29a, 0xf9, 0x15a, 0x30d, 0x26d, 640, 0x10c, 0x31a, 0x216, 
            0x21b, 0x30d, 0x198, 390, 0x284, 0x66, 0x1dc, 0x1f3, 290, 0x278, 0x221, 0x25, 0x35a, 0x394, 0x228, 0x29, 
            0x21e, 0x121, 0x7a, 0x110, 0x17f, 800, 0x1e5, 0x62, 0x2f0, 0x1d8, 0x2f9, 0x6b, 0x310, 860, 0x292, 0x2e5, 
            290, 0xcc, 0x2a9, 0x197, 0x357, 0x55, 0x63, 0x3e, 0x1e2, 180, 20, 0x129, 0x1c3, 0x251, 0x391, 0x8e, 
            0x328, 0x2ac, 0x11f, 0x218, 0x231, 0x4c, 0x28d, 0x383, 0x2d9, 0x237, 0x2e8, 390, 0x201, 0xc0, 0x204, 0x102, 
            240, 0x206, 0x31a, 0x18b, 0x300, 0x350, 0x33, 610, 0x180, 0xa8, 190, 0x33a, 0x148, 0x254, 0x312, 0x12f, 
            570, 0x17d, 0x19f, 0x281, 0x9c, 0xed, 0x97, 0x1ad, 0x213, 0xcf, 0x2a4, 710, 0x59, 0xa8, 0x130, 0x192, 
            40, 0x2c4, 0x23f, 0xa2, 0x360, 0xe5, 0x41, 0x35d, 0x349, 0x200, 0xa4, 0x1dd, 0xdd, 0x5c, 0x166, 0x311, 
            0x120, 0x165, 850, 0x344, 0x33b, 0x2e0, 0x2c3, 0x5e, 8, 0x1ee, 0x72, 0x209, 2, 0x1f3, 0x353, 0x21f, 
            0x98, 0x2d9, 0x303, 0x5f, 0xf8, 0x169, 0x242, 0x143, 0x358, 0x31d, 0x121, 0x33, 0x2ac, 0x1d2, 0x215, 820, 
            0x29d, 0x2d, 0x386, 0x1c4, 0xa7, 0x156, 0xf4, 0xad, 0x23, 0x1cf, 0x28b, 0x33, 0x2bb, 0x24f, 0x1c4, 0x242, 
            0x25, 0x7c, 0x12a, 0x14c, 0x228, 0x2b, 0x1ab, 0x77, 0x296, 0x309, 0x1db, 850, 0x2fc, 0x16c, 0x242, 0x38f, 
            0x11b, 0x2c7, 0x1d8, 420, 0xf5, 0x120, 0x252, 0x18a, 0x1ff, 0x147, 0x24d, 0x309, 0x2bb, 0x2b0, 0x2b, 0x198, 
            0x34a, 0x17f, 0x2d1, 0x209, 560, 0x284, 0x2ca, 0x22f, 0x3e, 0x91, 0x369, 0x297, 0x2c9, 0x9f, 0x2a0, 0x2d9, 
            0x270, 0x3b, 0xc1, 0x1a1, 0x9e, 0xd1, 0x233, 0x234, 0x157, 0x2b5, 0x6d, 0x260, 0x233, 0x16d, 0xb5, 0x304, 
            0x2a5, 310, 0xf8, 0x161, 0x2c4, 410, 0x243, 870, 0x269, 0x349, 0x278, 860, 0x121, 0x218, 0x23, 0x309, 
            0x26a, 0x24a, 0x1a8, 0x341, 0x4d, 0x255, 0x15a, 0x10d, 0x2f5, 0x278, 0x2b7, 0x2ef, 0x14b, 0xf7, 0xb8, 0x2d, 
            0x313, 680, 0x12, 0x42, 0x197, 0x171, 0x36, 0x1ec, 0xe4, 0x265, 830, 0x39a, 0x1b5, 0x207, 0x284, 0x389, 
            0x315, 420, 0x131, 0x1b9, 0xcf, 300, 0x37c, 0x33b, 0x8d, 0x219, 0x17d, 0x296, 0x201, 0x38, 0xfc, 0x155, 
            0xf2, 0x31d, 0x346, 0x345, 720, 0xe0, 0x133, 0x277, 0x3d, 0x57, 560, 310, 0x2f4, 0x299, 0x18d, 0x328, 
            0x353, 0x135, 0x1d9, 0x31b, 0x17a, 0x1f, 0x287, 0x393, 0x1cb, 0x326, 590, 0x2db, 0x1a9, 0xd8, 0x224, 0xf9, 
            0x141, 0x371, 0x2bb, 0x217, 0x2a1, 0x30e, 210, 0x32f, 0x389, 0x12f, 0x34b, 0x39a, 0x119, 0x49, 0x1d5, 0x317, 
            660, 0xa2, 0x1f2, 0x134, 0x9b, 0x1a6, 0x38b, 0x331, 0xbb, 0x3e, 0x10, 0x1a9, 0x217, 0x150, 0x11e, 0x1b5, 
            0x177, 0x111, 610, 0x128, 0xb7, 0x39b, 0x74, 0x29b, 0x2ef, 0x161, 0x3e, 0x16e, 0x2b3, 0x17b, 0x2af, 0x34a, 
            0x25, 0x165, 720, 0x2e6, 330, 5, 0x27, 0x39b, 0x137, 0x1a8, 0xf2, 0x2ed, 0x141, 0x36, 0x29d, 0x13c, 
            0x156, 0x12b, 0x216, 0x69, 0x29b, 0x1e8, 640, 0x2a0, 0x240, 540, 0x13c, 0x1e6, 0x2d1, 610, 0x2e, 0x290, 
            0x1bf, 0xab, 0x268, 0x1d0, 190, 0x213, 0x129, 0x141, 0x2fa, 0x2f0, 0x215, 0xaf, 0x86, 14, 0x17d, 0x1b1, 
            0x2cd, 0x2d, 0x6f, 20, 0x254, 0x11c, 0x2e0, 0x8a, 0x286, 0x19b, 0x36d, 0x29d, 0x8d, 0x397, 0x2d, 780, 
            0x197, 0xa4, 0x14c, 0x383, 0xa5, 0x2d6, 600, 0x145, 0x1f2, 0x28f, 0x165, 0x2f0, 0x300, 0xdf, 0x351, 0x287, 
            0x3f, 310, 0x35f, 0xfb, 0x16e, 0x130, 0x11a, 0x2e2, 0x2a3, 410, 0x185, 0xf4, 0x1f, 0x79, 0x12f, 0x107
         };
        public int errorLevel = 9;
        private const int ISBYTE = 0x100000;
        public bool isTruncated = false;
        private int lenCodewords = -1;
        private const int LL = 0x1b;
        private const int LOWER = 0x20000;
        private ArrayList m_list = new ArrayList();
        private const int MAX_DATA_CODEWORDS = 0x39e;
        private int maxRows;
        private const int MIXED = 0x40000;
        private const int ML = 0x1c;
        private const int MOD = 0x3a1;
        public int moduleheight = 30;
        public int modulewidth = 10;
        private const int NUMERIC_MODE = 0x386;
        private const int OUTPUT_ANSIFONTENCODING = 0x10;
        private const int OUTPUT_BINARY = 8;
        private const int OUTPUT_HTML = 4;
        private const int OUTPUT_POSTSCRIPT = 1;
        private const int OUTPUT_TEXT = 2;
        private const int PAL = 0x1d;
        private const int PDF417_AUTO_ERROR_LEVEL = 0;
        private const int PDF417_ERROR_EXCEED_MAX_ROWS = 4;
        private const int PDF417_ERROR_INVALID_PARAMS = 2;
        private const int PDF417_ERROR_SECURITY_LEVELL_TOO_HIGH = 3;
        private const int PDF417_ERROR_SUCCESS = 0;
        private const int PDF417_ERROR_TEXT_TOO_BIG = 1;
        private const int PDF417_FIXED_COLUMNS = 2;
        private const int PDF417_FIXED_RECTANGLE = 1;
        private const int PDF417_FIXED_ROWS = 4;
        private const int PDF417_INVERT_BITMAP = 0x80;
        private const int PDF417_USE_ASPECT_RATIO = 0;
        private const int PDF417_USE_ERROR_LEVEL = 0x10;
        private const int PDF417_USE_RAW_CODEWORDS = 0x40;
        private const int PL = 0x19;
        private const int PS = 0x1d;
        private const int PUNCTUATION = 0x80000;
        private const int SPACE = 0x1a;
        private const int START_CODE_SIZE = 0x11;
        private const int START_PATTERN = 0x1fea8;
        private const int STOP_PATTERN = 0x3fa29;
        private const int STOP_SIZE = 0x12;
        private const int TEXT_MODE = 900;
        private string textBuffer = "";

        public PDF417Encoder()
        {
            for (int i = 0; i < 0x3a0; i++)
            {
                this.codewords[i] = 0;
            }
            this.data = "PDF417 Barcode Demo";
            this.error = 0;
            this.dataLen = -1;
            this.errorLevel = -1;
            this.aspectRatio = 1;
            this.maxRows = 90;
            this.isTruncated = false;
            this.compactMode = 0;
        }

        private void assemble()
        {
            if (this.m_list.Count != 0)
            {
                this.codewordPtr = 1;
                for (int i = 0; i < this.m_list.Count; i++)
                {
                    ListElement element = (ListElement) this.m_list[i];
                    char type = element.type;
                    switch (type)
                    {
                        case 'B':
                            this.codewords[this.codewordPtr++] = ((element.getLength() % 6) > 0) ? 0x385 : 0x39c;
                            this.byteCompaction(element.start, element.getLength());
                            break;

                        case 'N':
                            if (this.compactMode == 1)
                            {
                                this.codewords[this.codewordPtr++] = 900;
                                this.textCompaction(element.start, element.getLength());
                            }
                            else if (this.compactMode == 3)
                            {
                                this.codewords[this.codewordPtr++] = ((element.getLength() % 6) > 0) ? 0x385 : 0x39c;
                                this.byteCompaction(element.start, element.getLength());
                            }
                            else
                            {
                                this.codewords[this.codewordPtr++] = 0x386;
                                this.numberCompaction(element.start, element.getLength());
                            }
                            break;

                        default:
                            if (type == 'T')
                            {
                                if (i != 0)
                                {
                                    this.codewords[this.codewordPtr++] = 900;
                                }
                                if (this.compactMode == 3)
                                {
                                    this.codewords[this.codewordPtr++] = ((element.getLength() % 6) > 0) ? 0x385 : 0x39c;
                                    this.byteCompaction(element.start, element.getLength());
                                }
                                else
                                {
                                    this.textCompaction(element.start, element.getLength());
                                }
                            }
                            break;
                    }
                    if (this.error > 0)
                    {
                        return;
                    }
                }
            }
        }

        private void basicNumberCompaction(int start, int length)
        {
            int[] numArray = new int[this.codewords.Length - this.codewordPtr];
            int index = length / 3;
            this.codewordPtr += index + 1;
            int codewordPtr = 0;
            for (codewordPtr = this.codewordPtr; codewordPtr < (this.codewords.Length - this.codewordPtr); codewordPtr++)
            {
                if ((codewordPtr - this.codewordPtr) <= ((index + 1) * 4))
                {
                    numArray[codewordPtr - this.codewordPtr] = 0;
                }
                else
                {
                    numArray[codewordPtr - this.codewordPtr] = this.codewords[codewordPtr];
                }
            }
            numArray[index] = 1;
            length += start;
            for (int i = start; i < length; i++)
            {
                int num3;
                int[] numArray2;
                IntPtr ptr;
                for (num3 = index; num3 >= 0; num3--)
                {
                    (numArray2 = numArray)[(int) (ptr = (IntPtr) num3)] = numArray2[(int) ptr] * 10;
                }
                (numArray2 = numArray)[(int) (ptr = (IntPtr) index)] = numArray2[(int) ptr] + (this.data[i] - '0');
                for (num3 = index; num3 > 0; num3--)
                {
                    numArray[(int) (ptr = (IntPtr) (num3 - 1))] = numArray[(int) ptr] + (numArray[num3] / 900);
                    numArray[num3] = numArray[num3] % 900;
                }
            }
            for (codewordPtr = 0; codewordPtr <= index; codewordPtr++)
            {
                this.codewords[((this.codewordPtr - index) + codewordPtr) - 1] = numArray[codewordPtr];
            }
        }

        private void breakString()
        {
            int start;
            int num6;
            bool flag;
            bool flag2;
            ListElement element;
            ListElement element2;
            ListElement element3;
            int dataLen = this.dataLen;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            char ch = '0';
            int num7 = 0;
            this.m_list.Clear();
            for (start = 0; start < dataLen; start++)
            {
                ch = this.data[start];
                if ((ch >= '0') && (ch <= '9'))
                {
                    if (num4 == 0)
                    {
                        num3 = start;
                    }
                    num4++;
                }
                else
                {
                    if (num4 >= 13)
                    {
                        if (num2 != num3)
                        {
                            ch = this.data[num2];
                            flag = (((ch >= ' ') && (ch < '\x007f')) || ((ch == '\r') || (ch == '\n'))) || (ch == '\t');
                            for (num6 = num2; num6 < num3; num6++)
                            {
                                ch = this.data[num6];
                                flag2 = (((ch >= ' ') && (ch < '\x007f')) || ((ch == '\r') || (ch == '\n'))) || (ch == '\t');
                                if (flag2 != flag)
                                {
                                    this.m_list.Add(new ListElement(flag ? 'T' : 'B', num2, num6));
                                    num2 = num6;
                                    flag = flag2;
                                }
                            }
                            this.m_list.Add(new ListElement(flag ? 'T' : 'B', num2, num3));
                        }
                        this.m_list.Add(new ListElement('N', num3, start));
                        num2 = start;
                    }
                    num4 = 0;
                }
            }
            if (num4 < 13)
            {
                num3 = dataLen;
            }
            if (num2 != num3)
            {
                ch = this.data[num2];
                flag = (((ch >= ' ') && (ch < '\x007f')) || ((ch == '\r') || (ch == '\n'))) || (ch == '\t');
                for (num6 = num2; num6 < num3; num6++)
                {
                    ch = this.data[num6];
                    flag2 = (((ch >= ' ') && (ch < '\x007f')) || ((ch == '\r') || (ch == '\n'))) || (ch == '\t');
                    if (flag2 != flag)
                    {
                        this.m_list.Add(new ListElement(flag ? 'T' : 'B', num2, num6));
                        num2 = num6;
                        flag = flag2;
                    }
                }
                this.m_list.Add(new ListElement(flag ? 'T' : 'B', num2, num3));
            }
            if (num4 >= 13)
            {
                this.m_list.Add(new ListElement('N', num3, dataLen));
            }
            for (start = 0; start < this.m_list.Count; start++)
            {
                element = (ListElement) this.m_list[start];
                if (start == 0)
                {
                    element2 = null;
                }
                else
                {
                    element2 = (ListElement) this.m_list[start - 1];
                }
                if ((start + 1) == this.m_list.Count)
                {
                    element3 = null;
                }
                else
                {
                    element3 = (ListElement) this.m_list[start + 1];
                }
                if (((element.isType('B') && (element.getLength() == 1)) && ((element2 != null) && (element3 != null))) && ((element2.isType('T') && element3.isType('T')) && ((element2.getLength() + element3.getLength()) >= 3)))
                {
                    element2.end = element3.end;
                    this.m_list.RemoveAt(start);
                    this.m_list.RemoveAt(start);
                    start = -1;
                }
            }
            for (start = 0; start < this.m_list.Count; start++)
            {
                element = (ListElement) this.m_list[start];
                if (start == 0)
                {
                    element2 = null;
                }
                else
                {
                    element2 = (ListElement) this.m_list[start - 1];
                }
                if ((start + 1) == this.m_list.Count)
                {
                    element3 = null;
                }
                else
                {
                    element3 = (ListElement) this.m_list[start + 1];
                }
                if (element.isType('T') && (element.getLength() >= 5))
                {
                    num7 = 0;
                    if ((element2 != null) && ((element2.isType('B') && (element2.getLength() == 1)) || element2.isType('T')))
                    {
                        num7 = 1;
                        element.start = element2.start;
                        this.m_list.RemoveAt(start - 1);
                        start--;
                    }
                    if ((element3 != null) && ((element3.isType('B') && (element3.getLength() == 1)) || element3.isType('T')))
                    {
                        num7 = 1;
                        element.end = element3.end;
                        this.m_list.RemoveAt(start + 1);
                    }
                    if (num7 > 0)
                    {
                        start = -1;
                    }
                }
            }
            for (start = 0; start < this.m_list.Count; start++)
            {
                element = (ListElement) this.m_list[start];
                if (start == 0)
                {
                    element2 = null;
                }
                else
                {
                    element2 = (ListElement) this.m_list[start - 1];
                }
                if ((start + 1) == this.m_list.Count)
                {
                    element3 = null;
                }
                else
                {
                    element3 = (ListElement) this.m_list[start + 1];
                }
                if (element.isType('B'))
                {
                    num7 = 0;
                    if ((element2 != null) && ((element2.isType('T') && (element2.getLength() < 5)) || element2.isType('B')))
                    {
                        num7 = 1;
                        element.start = element2.start;
                        this.m_list.RemoveAt(start - 1);
                        start--;
                    }
                    if ((element3 != null) && ((element3.isType('T') && (element3.getLength() < 5)) || element3.isType('B')))
                    {
                        num7 = 1;
                        element.end = element3.end;
                        this.m_list.RemoveAt(start + 1);
                    }
                    if (num7 == 1)
                    {
                        start = -1;
                    }
                }
            }
            if (this.m_list.Count == 1)
            {
                element = (ListElement) this.m_list[0];
                if ((element.type == 'T') && (element.getLength() >= 8))
                {
                    start = element.start;
                    while (start < element.end)
                    {
                        ch = this.data[start];
                        if ((ch < '0') || (ch > '9'))
                        {
                            break;
                        }
                        start++;
                    }
                    if (start == element.end)
                    {
                        element.type = 'N';
                    }
                }
            }
        }

        private void byteCompaction(int start, int length)
        {
            int num3 = ((length / 6) * 5) + (length % 6);
            if ((num3 + this.codewordPtr) > 0x39e)
            {
                this.error = 1;
            }
            else
            {
                length += start;
                for (int i = start; i < length; i += 6)
                {
                    num3 = ((length - i) < 0x2c) ? (length - i) : 6;
                    if (num3 < 6)
                    {
                        for (int j = 0; j < num3; j++)
                        {
                            this.codewords[this.codewordPtr++] = this.data[i + j] & '\x00ff';
                        }
                    }
                    else
                    {
                        this.byteCompaction6(i);
                    }
                }
            }
        }

        private void byteCompaction6(int start)
        {
            int index;
            int num = 6;
            int[] numArray = new int[this.codewords.Length - this.codewordPtr];
            for (index = this.codewordPtr; index < (this.codewords.Length - this.codewordPtr); index++)
            {
                numArray[index - this.codewordPtr] = this.codewords[index];
            }
            int num3 = 4;
            for (index = 0; index < ((num3 + 1) * 4); index++)
            {
                numArray[index] = 0;
            }
            num += start;
            for (int i = start; i < num; i++)
            {
                int num5;
                int[] numArray2;
                IntPtr ptr;
                for (num5 = num3; num5 >= 0; num5--)
                {
                    (numArray2 = numArray)[(int) (ptr = (IntPtr) num5)] = numArray2[(int) ptr] * 0x100;
                }
                (numArray2 = numArray)[(int) (ptr = (IntPtr) num3)] = numArray2[(int) ptr] + (this.data[i] & '\x00ff');
                for (num5 = num3; num5 > 0; num5--)
                {
                    numArray[(int) (ptr = (IntPtr) (num5 - 1))] = numArray[(int) ptr] + (numArray[num5] / 900);
                    numArray[num5] = numArray[num5] % 900;
                }
            }
            for (index = 0; index < 5; index++)
            {
                this.codewords[this.codewordPtr++] = numArray[index];
            }
        }

        private void calculateErrorCorrection(int dest)
        {
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int[] numArray2 = new int[this.codewords.Length - dest];
            if ((this.errorLevel < 0) || (this.errorLevel > 8))
            {
                this.errorLevel = 0;
            }
            int[] numArray = this.ERROR_LEVEL(this.errorLevel);
            int num4 = ((int) 2) << this.errorLevel;
            int index = 0;
            for (index = 0; index < (this.codewords.Length - dest); index++)
            {
                if (index < (num4 * 4))
                {
                    numArray2[index] = 0;
                }
                else
                {
                    numArray2[index] = this.codewords[index + dest];
                }
            }
            int num5 = num4 - 1;
            for (int i = 0; i < this.lenCodewords; i++)
            {
                num = this.codewords[i] + numArray2[0];
                for (int j = 0; j <= num5; j++)
                {
                    num2 = (num * numArray[num5 - j]) % 0x3a1;
                    num3 = 0x3a1 - num2;
                    numArray2[j] = (((j == num5) ? 0 : numArray2[j + 1]) + num3) % 0x3a1;
                }
            }
            for (int k = 0; k < num4; k++)
            {
                numArray2[k] = (0x3a1 - numArray2[k]) % 0x3a1;
            }
            for (index = 0; index < (this.codewords.Length - dest); index++)
            {
                this.codewords[index + dest] = numArray2[index];
            }
        }

        private string ConvertIntToBinaryString(long param, int len)
        {
            string text = "";
            long num = param;
            while (len-- > 0)
            {
                if ((num & 1) == 0)
                {
                    text = '0' + text;
                }
                else
                {
                    text = '1' + text;
                }
                if (num > 0)
                {
                    num = num >> 1;
                }
            }
            return text;
        }

        public string Encode(string str)
        {
            int num;
            string text2 = "";
            string text = this.TildeCodes(str);
            if (this.compactMode == 2)
            {
                for (num = 0; num < text.Length; num++)
                {
                    if ((text[num] >= '0') && (text[num] <= '9'))
                    {
                        text2 = text2 + text[num];
                    }
                }
            }
            else if (this.compactMode == 1)
            {
                for (num = 0; num < text.Length; num++)
                {
                    if (((text[num] >= ' ') && (text[num] <= '~')) || (((text[num] == '\t') || (text[num] == '\n')) || (text[num] == '\r')))
                    {
                        text2 = text2 + text[num];
                    }
                }
            }
            else if ((this.compactMode == 0) || (this.compactMode == 3))
            {
                text2 = text;
            }
            this.data = text2;
            this.dataLen = text2.Length;
            this.paintCode();
            int num2 = 0;
            this.textBuffer = "";
            for (int i = 0; i < this.codeRows; i++)
            {
                int index = i % 3;
                int[] numArray = this.CLUSTERS[index];
                this.textBuffer = this.textBuffer + this.ConvertIntToBinaryString((long) 0x1fea8, 0x11);
                int num6 = this.leftEdge(i, index);
                this.textBuffer = this.textBuffer + this.ConvertIntToBinaryString((long) numArray[num6], 0x11);
                for (int j = 0; j < this.codeColumns; j++)
                {
                    this.textBuffer = this.textBuffer + this.ConvertIntToBinaryString((long) numArray[this.codewords[num2++]], 0x11);
                }
                if (this.isTruncated)
                {
                    this.textBuffer = this.textBuffer + this.ConvertIntToBinaryString((long) 0x10000, 0x11);
                }
                else
                {
                    this.textBuffer = this.textBuffer + this.ConvertIntToBinaryString((long) numArray[this.rightEdge(i, index)], 0x11);
                    this.textBuffer = this.textBuffer + this.ConvertIntToBinaryString((long) 0x3fa29, 0x12);
                }
                this.textBuffer = this.textBuffer + "Z";
            }
            return this.textBuffer;
        }

        private int[] ERROR_LEVEL(int i)
        {
            int[] numArray = ERROR_LEVEL0;
            switch (i)
            {
                case 1:
                    return ERROR_LEVEL1;

                case 2:
                    return ERROR_LEVEL2;

                case 3:
                    return ERROR_LEVEL3;

                case 4:
                    return ERROR_LEVEL4;

                case 5:
                    return ERROR_LEVEL5;

                case 6:
                    return ERROR_LEVEL6;

                case 7:
                    return ERROR_LEVEL7;

                case 8:
                    return ERROR_LEVEL8;
            }
            return numArray;
        }

        private int getMaxSquare()
        {
            if (this.codeColumns > 0x15)
            {
                this.codeColumns = 0x1d;
                this.codeRows = 0x20;
            }
            else
            {
                this.codeColumns = 0x10;
                this.codeRows = 0x3a;
            }
            return 0x3a0;
        }

        private int getTextTypeAndValue(string data, int size, int idx)
        {
            string text = "0123456789&\r\t,:#-.$/+%*=^";
            string text2 = ";<>@[\\]_`~!\r\t,:\n-.$/\"|*()?{}'";
            if (idx >= size)
            {
                return 0;
            }
            char ch = data[idx];
            if ((ch >= 'A') && (ch <= 'Z'))
            {
                return ((0x10000 + ch) - 0x41);
            }
            if ((ch >= 'a') && (ch <= 'z'))
            {
                return ((0x20000 + ch) - 0x61);
            }
            if (ch == ' ')
            {
                return 0x7001a;
            }
            int index = text.IndexOf(ch);
            int num2 = text2.IndexOf(ch);
            if ((index < 0) && (num2 < 0))
            {
                return (0x100000 + (ch & '\x00ff'));
            }
            if (index == num2)
            {
                return (0xc0000 + index);
            }
            if (index >= 0)
            {
                return (0x40000 + index);
            }
            return (0x80000 + num2);
        }

        private int leftEdge(int row, int rowMod)
        {
            switch (rowMod)
            {
                case 0:
                    return ((30 * (row / 3)) + ((this.codeRows - 1) / 3));

                case 1:
                    return (((30 * (row / 3)) + (this.errorLevel * 3)) + ((this.codeRows - 1) % 3));
            }
            return (((30 * (row / 3)) + this.codeColumns) - 1);
        }

        private int maxPossibleErrorLevel(int remain)
        {
            int num = 8;
            for (int i = 0x200; num > 0; i = i >> 1)
            {
                if (remain >= i)
                {
                    return num;
                }
                num--;
            }
            return 0;
        }

        private void numberCompaction(int start, int length)
        {
            int num = (length / 0x2c) * 15;
            int num2 = length % 0x2c;
            if (num2 == 0)
            {
                num2 = num;
            }
            else
            {
                num2 = (num + (num2 / 3)) + 1;
            }
            if ((num2 + this.codewordPtr) > 0x39e)
            {
                this.error = 1;
            }
            else
            {
                length += start;
                for (int i = start; i < length; i += 0x2c)
                {
                    num2 = ((length - i) < 0x2c) ? (length - i) : 0x2c;
                    this.basicNumberCompaction(i, num2);
                }
            }
        }

        private void paintCode()
        {
            int index;
            bool flag = false;
            bool flag2 = false;
            bool flag3 = true;
            if (this.errorLevel == 9)
            {
                flag3 = false;
            }
            if (this.codeRows > 0)
            {
                flag2 = true;
            }
            if (this.codeColumns > 0)
            {
                flag = true;
            }
            this.error = 0;
            for (index = 0; index < 0x3a0; index++)
            {
                this.codewords[index] = 0;
            }
            if (this.dataLen < 0)
            {
                this.dataLen = this.data.Length;
            }
            if (this.dataLen > 0x152c)
            {
                this.error = 1;
            }
            else
            {
                this.breakString();
                for (index = 0; index < 0x3a0; index++)
                {
                    this.codewords[index] = 0;
                }
                this.assemble();
                if (this.error <= 0)
                {
                    int num;
                    this.codewords[0] = this.lenCodewords = this.codewordPtr;
                    int num2 = this.maxPossibleErrorLevel(0x3a0 - this.lenCodewords);
                    if (this.lenCodewords < 0x29)
                    {
                        num = 2;
                    }
                    else if (this.lenCodewords < 0xa1)
                    {
                        num = 3;
                    }
                    else if (this.lenCodewords < 0x141)
                    {
                        num = 4;
                    }
                    else
                    {
                        num = 5;
                    }
                    if (flag3 && (this.errorLevel > num2))
                    {
                        this.error = 3;
                    }
                    else
                    {
                        if (!flag3)
                        {
                            this.errorLevel = num;
                        }
                        if (this.errorLevel < 0)
                        {
                            this.errorLevel = 0;
                        }
                        else if (this.errorLevel > num2)
                        {
                            this.errorLevel = num2;
                        }
                        if (this.codeColumns < 1)
                        {
                            this.codeColumns = 1;
                        }
                        else if (this.codeColumns > 30)
                        {
                            this.codeColumns = 30;
                        }
                        if (this.codeRows < 3)
                        {
                            this.codeRows = 3;
                        }
                        else if (this.codeRows > this.maxRows)
                        {
                            this.codeRows = this.maxRows;
                        }
                        int num3 = ((int) 2) << this.errorLevel;
                        int num4 = this.lenCodewords + num3;
                        if (flag && flag2)
                        {
                            num4 = this.codeColumns * this.codeRows;
                            if (num4 > 0x3a0)
                            {
                                num4 = this.getMaxSquare();
                            }
                        }
                        else if (!flag)
                        {
                            if (this.aspectRatio < 0.001)
                            {
                                this.aspectRatio = 0.0010000000474974513;
                            }
                            else if (this.aspectRatio > 1000)
                            {
                                this.aspectRatio = 1000;
                            }
                            double num7 = (this.moduleheight * 1) / (this.modulewidth * 1);
                            this.codeColumns = (int) (Math.Sqrt((this.aspectRatio * num4) / ((double) ((float) (17 / num7)))) + 0.5);
                            if (this.codeColumns < 1)
                            {
                                this.codeColumns = 1;
                            }
                            else if (this.codeColumns > 30)
                            {
                                this.codeColumns = 30;
                            }
                            this.codeRows = ((num4 - 1) / this.codeColumns) + 1;
                            if (this.codeRows < 3)
                            {
                                this.codeRows = 3;
                            }
                            else if (this.codeRows > this.maxRows)
                            {
                                this.codeRows = this.maxRows;
                                this.codeColumns = ((num4 - 1) / this.maxRows) + 1;
                            }
                        }
                        else
                        {
                            this.codeRows = ((num4 - 1) / this.codeColumns) + 1;
                            if (this.codeRows < 3)
                            {
                                this.codeRows = 3;
                            }
                            else if (this.codeRows > this.maxRows)
                            {
                                this.error = 4;
                                return;
                            }
                        }
                        num4 = this.codeRows * this.codeColumns;
                        if (num4 > 0x3a0)
                        {
                            num4 = this.getMaxSquare();
                        }
                        if (flag3)
                        {
                            num3 = ((int) 2) << this.errorLevel;
                        }
                        else
                        {
                            this.errorLevel = this.maxPossibleErrorLevel(num4 - this.lenCodewords);
                            num3 = ((int) 2) << this.errorLevel;
                        }
                        int num5 = (num4 - num3) - this.lenCodewords;
                        this.codewordPtr = this.lenCodewords;
                        while (num5-- > 0)
                        {
                            this.codewords[this.codewordPtr++] = 900;
                        }
                        this.codewords[0] = this.lenCodewords = this.codewordPtr;
                        this.calculateErrorCorrection(this.lenCodewords);
                        this.lenCodewords = num4;
                    }
                }
            }
        }

        private int rightEdge(int row, int rowMod)
        {
            switch (rowMod)
            {
                case 0:
                    return (((30 * (row / 3)) + this.codeColumns) - 1);

                case 1:
                    return ((30 * (row / 3)) + ((this.codeRows - 1) / 3));
            }
            return (((30 * (row / 3)) + (this.errorLevel * 3)) + ((this.codeRows - 1) % 3));
        }

        private void textCompaction(int start, int length)
        {
            int[] numArray = new int[0x2a58];
            int num = 0x10000;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            length += start;
            for (int i = start; i < length; i++)
            {
                num4 = this.getTextTypeAndValue(this.data, length, i);
                if ((num4 & num) != 0)
                {
                    numArray[num2++] = num4 & 0xff;
                }
                else if ((num4 & 0x100000) != 0)
                {
                    if ((num2 & 1) != 0)
                    {
                        numArray[num2++] = ((num & 0x80000) != 0) ? 0x1d : 0x1d;
                        num = ((num & 0x80000) != 0) ? 0x10000 : num;
                    }
                    numArray[num2++] = 0x391;
                    numArray[num2++] = num4 & 0xff;
                    num3 += 2;
                }
                else
                {
                    switch (num)
                    {
                        case 0x40000:
                        {
                            if ((num4 & 0x20000) != 0)
                            {
                                numArray[num2++] = 0x1b;
                                numArray[num2++] = num4 & 0xff;
                                num = 0x20000;
                            }
                            else if ((num4 & 0x10000) != 0)
                            {
                                numArray[num2++] = 0x1c;
                                numArray[num2++] = num4 & 0xff;
                                num = 0x10000;
                            }
                            else if (((this.getTextTypeAndValue(this.data, length, i + 1) & this.getTextTypeAndValue(this.data, length, i + 2)) & 0x80000) != 0)
                            {
                                numArray[num2++] = 0x19;
                                numArray[num2++] = num4 & 0xff;
                                num = 0x80000;
                            }
                            else
                            {
                                numArray[num2++] = 0x1d;
                                numArray[num2++] = num4 & 0xff;
                            }
                            continue;
                        }
                        case 0x80000:
                        {
                            numArray[num2++] = 0x1d;
                            num = 0x10000;
                            i--;
                            continue;
                        }
                        case 0x10000:
                        {
                            if ((num4 & 0x20000) != 0)
                            {
                                numArray[num2++] = 0x1b;
                                numArray[num2++] = num4 & 0xff;
                                num = 0x20000;
                            }
                            else if ((num4 & 0x40000) != 0)
                            {
                                numArray[num2++] = 0x1c;
                                numArray[num2++] = num4 & 0xff;
                                num = 0x40000;
                            }
                            else if (((this.getTextTypeAndValue(this.data, length, i + 1) & this.getTextTypeAndValue(this.data, length, i + 2)) & 0x80000) != 0)
                            {
                                numArray[num2++] = 0x1c;
                                numArray[num2++] = 0x19;
                                numArray[num2++] = num4 & 0xff;
                                num = 0x80000;
                            }
                            else
                            {
                                numArray[num2++] = 0x1d;
                                numArray[num2++] = num4 & 0xff;
                            }
                            continue;
                        }
                        case 0x20000:
                        {
                            if ((num4 & 0x10000) == 0)
                            {
                                goto Label_0228;
                            }
                            if (((this.getTextTypeAndValue(this.data, length, i + 1) & this.getTextTypeAndValue(this.data, length, i + 2)) & 0x10000) != 0)
                            {
                                numArray[num2++] = 0x1c;
                                numArray[num2++] = 0x1c;
                                num = 0x10000;
                            }
                            else
                            {
                                numArray[num2++] = 0x1b;
                            }
                            numArray[num2++] = num4 & 0xff;
                            continue;
                        }
                    }
                }
                continue;
            Label_0228:
                if ((num4 & 0x40000) != 0)
                {
                    numArray[num2++] = 0x1c;
                    numArray[num2++] = num4 & 0xff;
                    num = 0x40000;
                }
                else if (((this.getTextTypeAndValue(this.data, length, i + 1) & this.getTextTypeAndValue(this.data, length, i + 2)) & 0x80000) != 0)
                {
                    numArray[num2++] = 0x1c;
                    numArray[num2++] = 0x19;
                    numArray[num2++] = num4 & 0xff;
                    num = 0x80000;
                }
                else
                {
                    numArray[num2++] = 0x1d;
                    numArray[num2++] = num4 & 0xff;
                }
            }
            if ((num2 & 1) != 0)
            {
                numArray[num2++] = 0x1d;
            }
            int num6 = (num2 + num3) / 2;
            if ((num6 + this.codewordPtr) > 0x39e)
            {
                this.error = 1;
            }
            else
            {
                length = num2;
                num2 = 0;
                while (num2 < length)
                {
                    num4 = numArray[num2++];
                    if (num4 >= 30)
                    {
                        this.codewords[this.codewordPtr++] = num4;
                        this.codewords[this.codewordPtr++] = numArray[num2++];
                    }
                    else
                    {
                        this.codewords[this.codewordPtr++] = (num4 * 30) + numArray[num2++];
                    }
                }
            }
        }

        protected string TildeCodes(string inpara)
        {
            string text = "";
            for (int i = 0; i < inpara.Length; i++)
            {
                if (inpara[i] == '~')
                {
                    int num = inpara[i + 1] - '@';
                    if ((num > 0) && (num < 0x1b))
                    {
                        text = text + ((char) num);
                        i++;
                    }
                    else if ((((inpara.Length - i) > 2) && char.IsNumber(inpara[i + 1])) && (char.IsNumber(inpara[i + 2]) && char.IsNumber(inpara[i + 3])))
                    {
                        num = (((100 * (inpara[i + 1] - '0')) + (10 * (inpara[i + 2] - '0'))) + inpara[i + 3]) - 0x30;
                        if (num < 0x100)
                        {
                            text = text + ((char) num);
                            i += 3;
                        }
                        else
                        {
                            text = text + inpara[i + 1];
                            i++;
                        }
                    }
                    else if (inpara[i + 1] == '1')
                    {
                        text = text + '\x00c8';
                        i++;
                    }
                    else if (inpara[i + 1] == '~')
                    {
                        text = text + '~';
                        i++;
                    }
                    else
                    {
                        text = text + '~';
                    }
                }
                else
                {
                    text = text + inpara[i];
                }
            }
            return text;
        }

        private class ListElement
        {
            public int end;
            public int start;
            public char type;

            public ListElement(char _type, int _start, int _end)
            {
                this.type = _type;
                this.start = _start;
                this.end = _end;
            }

            public int getLength()
            {
                return (this.end - this.start);
            }

            public bool isType(char _type)
            {
                return (this.type == _type);
            }

            public void setEnd(int _end)
            {
                this.end = _end;
            }
        }
    }
}

