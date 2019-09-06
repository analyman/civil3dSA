using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

public sealed class CodesSpecific
{
	public struct CodeType
	{
		public string Code;

		public int Index;

		public string Description;
	}

	public struct AllCodes
	{
		public bool CodesStructureFilled;

		public CodeType Crown;

		public CodeType CrownPave1;

		public CodeType CrownPave2;

		public CodeType CrownBase;

		public CodeType CrownSub;

		public CodeType ETW;

		public CodeType ETWPave1;

		public CodeType ETWPave2;

		public CodeType ETWBase;

		public CodeType ETWSub;

		public CodeType Lane;

		public CodeType LanePave1;

		public CodeType LanePave2;

		public CodeType LaneBase;

		public CodeType LaneSub;

		public CodeType EPS;

		public CodeType EPSPave1;

		public CodeType EPSPave2;

		public CodeType EPSBase;

		public CodeType EPSSub;

		public CodeType EPSBaseIn;

		public CodeType EPSSubIn;

		public CodeType ESUnpaved;

		public CodeType DaylightSub;

		public CodeType Daylight;

		public CodeType DaylightFill;

		public CodeType DaylightCut;

		public CodeType DitchIn;

		public CodeType DitchOut;

		public CodeType BenchIn;

		public CodeType BenchOut;

		public CodeType FlowlineDitch;

		public CodeType LMedDitch;

		public CodeType RMedDitch;

		public CodeType Flange;

		public CodeType Flowline_Gutter;

		public CodeType TopCurb;

		public CodeType BottomCurb;

		public CodeType BackCurb;

		public CodeType SidewalkIn;

		public CodeType SidewalkOut;

		public CodeType HingeCut;

		public CodeType HingeFill;

		public CodeType Top;

		public CodeType Datum;

		public CodeType Pave;

		public CodeType Pave1;

		public CodeType Pave2;

		public CodeType Base;

		public CodeType SubBase;

		public CodeType Gravel;

		public CodeType TopCurbNew;

		public CodeType BackCurbNew;

		public CodeType Curb;

		public CodeType Sidewalk;

		public CodeType Hinge;

		public CodeType EOV;

		public CodeType EOVOverlay;

		public CodeType Level;

		public CodeType Mill;

		public CodeType Overlay;

		public CodeType CrownOverlay;

		public CodeType Barrier;

		public CodeType EBD;

		public CodeType CrownDeck;

		public CodeType Deck;

		public CodeType Girder;

		public CodeType EBS;

		public CodeType ESL;

		public CodeType DaylightBallast;

		public CodeType ESBS;

		public CodeType DaylightSubballast;

		public CodeType Ballast;

		public CodeType Sleeper;

		public CodeType Subballast;

		public CodeType Rail;

		public CodeType R1;

		public CodeType R2;

		public CodeType R3;

		public CodeType R4;

		public CodeType R5;

		public CodeType R6;

		public CodeType Bridge;

		public CodeType Ditch;

		public CodeType CrownFin;

		public CodeType CrownSubBase;

		public CodeType ETWSubBase;

		public CodeType MarkedPoint;

		public CodeType Guardrail;

		public CodeType Median;

		public CodeType ETWOverlay;

		public CodeType TrenchBottom;

		public CodeType TrenchDaylight;

		public CodeType TrenchBedding;

		public CodeType TrenchBackfill;

		public CodeType Trench;

		public CodeType LaneBreak;

		public CodeType LaneBreakOverlay;

		public CodeType Sod;

		public CodeType DaylightStrip;

		public CodeType sForeslopeStripping;

		public CodeType Stripping;

		public CodeType ChannelFlowline;

		public CodeType Channe_Bottom;

		public CodeType ChannelTop;

		public CodeType ChannelExtension;

		public CodeType ChannelBackslope;

		public CodeType LiningMaterial;

		public CodeType DitchBack;

		public CodeType DitchFace;

		public CodeType DitchTop;

		public CodeType DitchBottom;

		public CodeType Backfill;

		public CodeType BackfillFace;

		public CodeType DitchLidFace;

		public CodeType LidTop;

		public CodeType DitchBackFill;

		public CodeType Lid;

		public CodeType DrainBottom;

		public CodeType DrainBottomOutside;

		public CodeType DrainTopOutside;

		public CodeType DrainTopInside;

		public CodeType DrainBottomInside;

		public CodeType DrainCenter;

		public CodeType FlowLine;

		public CodeType DrainTop;

		public CodeType DrainStructure;

		public CodeType DrainArea;

		public CodeType RWFront;

		public CodeType RWTop;

		public CodeType RWBack;

		public CodeType RWHinge;

		public CodeType RWInside;

		public CodeType RWOutside;

		public CodeType Wall;

		public CodeType RWall;

		public CodeType RWallB1;

		public CodeType RWallB2;

		public CodeType RWallB3;

		public CodeType RWallB4;

		public CodeType RWallK1;

		public CodeType RWallK2;

		public CodeType FootingBottom;

		public CodeType WalkEdge;

		public CodeType Lot;

		public CodeType Slope_Link;

		public CodeType Channel_Side;

		public CodeType Bench;

		public CodeType CrownPave3;

		public CodeType LanePave3;

		public CodeType ETWBase1;

		public CodeType CrownBase1;

		public CodeType LaneBase1;

		public CodeType ETWBase2;

		public CodeType CrownBase2;

		public CodeType LaneBase2;

		public CodeType ETWBase3;

		public CodeType CrownBase3;

		public CodeType LaneBase3;

		public CodeType ETWSub1;

		public CodeType CrownSub1;

		public CodeType LaneSub1;

		public CodeType ETWSub2;

		public CodeType CrownSub2;

		public CodeType LaneSub2;

		public CodeType ETWSub3;

		public CodeType CrownSub3;

		public CodeType LaneSub3;

		public CodeType Pave3;

		public CodeType Base1;

		public CodeType Base2;

		public CodeType Base3;

		public CodeType Subbase1;

		public CodeType Subbase2;

		public CodeType Subbase3;

		public CodeType EPSBase1;

		public CodeType EPSBase2;

		public CodeType EPSBase3;

		public CodeType EPSSubBase1;

		public CodeType EPSSubBase2;

		public CodeType EPSSubBase3;

		public CodeType ETWPave3;

		public CodeType EPSBase4;

		public CodeType Base4;

		public CodeType SR;

		public CodeType EPSPave3;
	}

	private const string constCodesFile = "C3DStockSubassemblyScripts.codes";

	private static string[] CodesDefault = new string[187];

	public static AllCodes Codes;

	[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
	private static extern int GetPrivateProfileString([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpAppName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpKeyName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpDefault, StringBuilder lpReturnedString, int nSize, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpFileName);

	private static void InitializeDefaults()
	{
		CodesDefault[1] = "Crown";
		CodesDefault[2] = "Crown_Pave1";
		CodesDefault[3] = "Crown_Pave2";
		CodesDefault[4] = "Crown_Base";
		CodesDefault[5] = "Crown_Sub";
		CodesDefault[6] = "ETW";
		CodesDefault[7] = "ETW_Pave1";
		CodesDefault[8] = "ETW_Pave2";
		CodesDefault[9] = "ETW_Base";
		CodesDefault[10] = "ETW_Sub";
		CodesDefault[11] = "Lane";
		CodesDefault[12] = "Lane_Pave1";
		CodesDefault[13] = "Lane_Pave2";
		CodesDefault[14] = "Lane_Base";
		CodesDefault[15] = "Lane_Sub";
		CodesDefault[16] = "EPS";
		CodesDefault[17] = "EPS_Pave1";
		CodesDefault[18] = "EPS_Pave2";
		CodesDefault[19] = "EPS_Base";
		CodesDefault[20] = "EPS_Sub";
		CodesDefault[21] = "EPS_Base_In";
		CodesDefault[22] = "EPS_Sub_In";
		CodesDefault[23] = "ES_Unpaved";
		CodesDefault[24] = "Daylight_Sub";
		CodesDefault[25] = "Daylight";
		CodesDefault[26] = "Daylight_Fill";
		CodesDefault[27] = "Daylight_Cut";
		CodesDefault[28] = "Ditch_In";
		CodesDefault[29] = "Ditch_Out";
		CodesDefault[30] = "Bench_In";
		CodesDefault[31] = "Bench_Out";
		CodesDefault[32] = "Flowline_Ditch";
		CodesDefault[33] = "LMedDitch";
		CodesDefault[34] = "RMedDitch";
		CodesDefault[35] = "Flange";
		CodesDefault[36] = "Flowline_Gutter";
		CodesDefault[37] = "Top_Curb";
		CodesDefault[38] = "Bottom_Curb";
		CodesDefault[39] = "Back_Curb";
		CodesDefault[40] = "Sidewalk_In";
		CodesDefault[41] = "Sidewalk_Out";
		CodesDefault[42] = "Hinge_Cut";
		CodesDefault[43] = "Hinge_Fill";
		CodesDefault[44] = "Top";
		CodesDefault[45] = "Datum";
		CodesDefault[46] = "Pave";
		CodesDefault[47] = "Pave1";
		CodesDefault[48] = "Pave2";
		CodesDefault[49] = "Base";
		CodesDefault[50] = "SubBase";
		CodesDefault[51] = "Gravel";
		CodesDefault[52] = "Top_Curb";
		CodesDefault[53] = "Back_Curb";
		CodesDefault[54] = "Curb";
		CodesDefault[55] = "Sidewalk";
		CodesDefault[56] = "Hinge";
		CodesDefault[57] = "EOV";
		CodesDefault[58] = "EOV_Overlay";
		CodesDefault[59] = "Level";
		CodesDefault[60] = "Mill";
		CodesDefault[61] = "Overlay";
		CodesDefault[62] = "Crown_Overlay";
		CodesDefault[63] = "Barrier";
		CodesDefault[64] = "EBD";
		CodesDefault[65] = "Crown_Deck";
		CodesDefault[66] = "Deck";
		CodesDefault[67] = "Girder";
		CodesDefault[68] = "EBS";
		CodesDefault[69] = "ESL";
		CodesDefault[70] = "Daylight_Ballast";
		CodesDefault[71] = "ESBS";
		CodesDefault[72] = "Daylight_Subballast";
		CodesDefault[73] = "Ballast";
		CodesDefault[74] = "Sleeper";
		CodesDefault[75] = "Subballast";
		CodesDefault[76] = "Rail";
		CodesDefault[77] = "R1";
		CodesDefault[78] = "R2";
		CodesDefault[79] = "R3";
		CodesDefault[80] = "R4";
		CodesDefault[81] = "R5";
		CodesDefault[82] = "R6";
		CodesDefault[83] = "Bridge";
		CodesDefault[84] = "Ditch";
		CodesDefault[85] = "Crown_Fin";
		CodesDefault[86] = "Crown_SubBase";
		CodesDefault[87] = "ETW_SubBase";
		CodesDefault[88] = "MarkedPoint";
		CodesDefault[89] = "Guardrail";
		CodesDefault[90] = "Median";
		CodesDefault[91] = "ETW_Overlay";
		CodesDefault[92] = "Trench_Bottom";
		CodesDefault[93] = "Trench_Daylight";
		CodesDefault[94] = "Trench_Bedding";
		CodesDefault[95] = "Trench_Backfill";
		CodesDefault[96] = "Trench";
		CodesDefault[97] = "LaneBreak";
		CodesDefault[98] = "LaneBreak_Overlay";
		CodesDefault[99] = "Sod";
		CodesDefault[100] = "Daylight_Strip";
		CodesDefault[101] = "Foreslope_Stripping";
		CodesDefault[102] = "Stripping";
		CodesDefault[103] = "Channel_Flowline";
		CodesDefault[104] = "Channel_Bottom";
		CodesDefault[105] = "Channel_Top";
		CodesDefault[106] = "Channel_Extension";
		CodesDefault[107] = "Channel_Backslope";
		CodesDefault[108] = "Lining_Material";
		CodesDefault[109] = "Ditch_Back";
		CodesDefault[110] = "Ditch_Face";
		CodesDefault[111] = "Ditch_Top";
		CodesDefault[112] = "Ditch_Bottom";
		CodesDefault[113] = "Backfill";
		CodesDefault[114] = "Backfill_Face";
		CodesDefault[115] = "Ditch_Lid_Face";
		CodesDefault[116] = "Lid_Top";
		CodesDefault[117] = "Ditch_Back_Fill";
		CodesDefault[118] = "Lid";
		CodesDefault[119] = "Drain_Bottom";
		CodesDefault[120] = "Drain_Top_Outside";
		CodesDefault[121] = "Drain_Top_Outside";
		CodesDefault[122] = "Drain_Top_Inside";
		CodesDefault[123] = "Drain_Bottom_Inside";
		CodesDefault[124] = "Drain_Center";
		CodesDefault[125] = "Flow_Line";
		CodesDefault[126] = "Drain_Top";
		CodesDefault[127] = "Drain_Structure";
		CodesDefault[128] = "Drain_Area";
		CodesDefault[129] = "RW_Front";
		CodesDefault[130] = "RW_Top";
		CodesDefault[131] = "RW_Back";
		CodesDefault[132] = "RW_Hinge";
		CodesDefault[133] = "RW_Inside";
		CodesDefault[134] = "RW_Outside";
		CodesDefault[135] = "Wall";
		CodesDefault[136] = "RWall";
		CodesDefault[137] = "RWall_B1";
		CodesDefault[138] = "RWall_B2";
		CodesDefault[139] = "RWall_B3";
		CodesDefault[140] = "RWall_B4";
		CodesDefault[141] = "RWall_K1";
		CodesDefault[142] = "RWall_K2";
		CodesDefault[143] = "Footing_Bottom";
		CodesDefault[144] = "Walk_Edge";
		CodesDefault[145] = "Lot";
		CodesDefault[146] = "Slope_Link";
		CodesDefault[147] = "Channel_Side";
		CodesDefault[148] = "Bench";
		CodesDefault[149] = "Crown_Pave3";
		CodesDefault[150] = "Lane_Pave3";
		CodesDefault[151] = "ETW_Base1";
		CodesDefault[152] = "Crown_Base1";
		CodesDefault[153] = "Lane_Base1";
		CodesDefault[154] = "ETW_Base2";
		CodesDefault[155] = "Crown_Base2";
		CodesDefault[156] = "Lane_Base2";
		CodesDefault[157] = "ETW_Base3";
		CodesDefault[158] = "Crown_Base3";
		CodesDefault[159] = "Lane_Base3";
		CodesDefault[160] = "ETW_Sub1";
		CodesDefault[161] = "Crown_Sub1";
		CodesDefault[162] = "Lane_Sub1";
		CodesDefault[163] = "ETW_Sub2";
		CodesDefault[164] = "Crown_Sub2";
		CodesDefault[165] = "Lane_Sub2";
		CodesDefault[166] = "ETW_Sub3";
		CodesDefault[167] = "Crown_Sub3";
		CodesDefault[168] = "Lane_Sub3";
		CodesDefault[169] = "Pave3";
		CodesDefault[170] = "Base1";
		CodesDefault[171] = "Base2";
		CodesDefault[172] = "Base3";
		CodesDefault[173] = "Subbase1";
		CodesDefault[174] = "Subbase2";
		CodesDefault[175] = "Subbase3";
		CodesDefault[176] = "EPS_Base1";
		CodesDefault[177] = "EPS_Base2";
		CodesDefault[178] = "EPS_Base3";
		CodesDefault[179] = "EPS_SubBase1";
		CodesDefault[180] = "EPS_SubBase2";
		CodesDefault[181] = "EPS_SubBase3";
		CodesDefault[182] = "ETW_Pave3";
		CodesDefault[183] = "EPS_Base4";
		CodesDefault[184] = "Base4";
		CodesDefault[185] = "SR";
		CodesDefault[186] = "EPS_Pave3";
	}

	private static void FillDefaults(Collection colCodesAndDescriptionHashtable)
	{
		int num = default(int);
		int num3 = default(int);
		try
		{
			ProjectData.ClearProjectError();
			num = -2;
			InitializeDefaults();
			int num2 = Information.UBound(CodesDefault);
			for (int i = 1; i <= num2; i = checked(i + 1))
			{
				colCodesAndDescriptionHashtable.Add(CodesDefault[i], "I" + Conversions.ToString(i));
			}
		}
		catch (object obj) when ((obj is Exception && num != 0) & (num3 == 0))
		{
			ProjectData.SetProjectError((Exception)obj);
			/*Error near IL_00b5: Could not find block for branch target IL_007d*/;
		}
		if (num3 != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private static string GetCodesFilePath()
	{
		string codesFilePath;
		try
		{
			codesFilePath = GetCodesFilePathFromIniFile();
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			codesFilePath = GetConstCodesFilePath();
			ProjectData.ClearProjectError();
		}
		if (File.Exists(codesFilePath))
		{
			return codesFilePath;
		}
		return GetConstCodesFilePath();
	}

	private static string GetCodesFilePathFromIniFile()
	{
		string contentDir = Interaction.Environ("AeccContent_Dir");
		if (Operators.CompareString(contentDir, null, TextCompare: false) == 0)
		{
			return "";
		}
		string codeIniFileName = "";
		if (Operators.CompareString(contentDir, "", TextCompare: false) != 0)
		{
			if (contentDir.LastIndexOf("\\") < checked(contentDir.Length - 1))
			{
				contentDir += "\\";
			}
			codeIniFileName = contentDir + "CodeFileName.ini";
		}
		string codeFileName = "";
		if (File.Exists(codeIniFileName))
		{
			StringBuilder sb = new StringBuilder(600);
			string lpAppName = "C3D";
			string lpKeyName = "CodeFileName";
			string lpDefault = "";
			GetPrivateProfileString(ref lpAppName, ref lpKeyName, ref lpDefault, sb, sb.Capacity, ref codeIniFileName);
			codeFileName = sb.ToString();
		}
		if (File.Exists(codeFileName))
		{
			return codeFileName;
		}
		return "";
	}

	private static string GetConstCodesFilePath()
	{
		string codes_file_path = Interaction.Environ("AeccContent_Dir");
		string GetConstCodesFilePath = (Operators.CompareString(codes_file_path, null, TextCompare: false) == 0) ? "" : codes_file_path;
		int lastBackSlashPosition = Strings.InStrRev(GetConstCodesFilePath, "\\");
		if (lastBackSlashPosition < GetConstCodesFilePath.Length)
		{
			return GetConstCodesFilePath + "\\C3DStockSubassemblyScripts.codes";
		}
		return GetConstCodesFilePath + "C3DStockSubassemblyScripts.codes";
	}

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	public static void FillCodeStructure()
	{
		int num = default(int);
		int num2 = default(int);
		try
		{
			ProjectData.ClearProjectError();
			num = -2;
			string sCodesFilePath = GetCodesFilePath();
			Collection colCodesAndDescriptionHashtable = new Collection();
			int i = 0;
			if (Strings.Len(Microsoft.VisualBasic.FileSystem.Dir(sCodesFilePath)) != 0)
			{
				TextFieldParser parser = new TextFieldParser(sCodesFilePath, Encoding.Default);
				parser.SetDelimiters(",");
				while (!parser.EndOfData)
				{
					string currentRow = parser.ReadLine();
					if (currentRow.IndexOf(",") != -1)
					{
						string strIndex = currentRow.Substring(0, currentRow.IndexOf(","));
						string sCodeAndDescription = currentRow.Substring(checked(currentRow.IndexOf(",") + 1));
						colCodesAndDescriptionHashtable.Add(sCodeAndDescription, "I" + strIndex);
					}
				}
			}
			else
			{
				FillDefaults(colCodesAndDescriptionHashtable);
			}
			Codes.CodesStructureFilled = true;
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Crown);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.CrownPave1);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.CrownPave2);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.CrownBase);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.CrownSub);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.ETW);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.ETWPave1);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.ETWPave2);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.ETWBase);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.ETWSub);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Lane);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.LanePave1);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.LanePave2);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.LaneBase);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.LaneSub);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.EPS);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.EPSPave1);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.EPSPave2);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.EPSBase);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.EPSSub);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.EPSBaseIn);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.EPSSubIn);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.ESUnpaved);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.DaylightSub);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Daylight);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.DaylightFill);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.DaylightCut);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.DitchIn);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.DitchOut);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.BenchIn);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.BenchOut);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.FlowlineDitch);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.LMedDitch);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.RMedDitch);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Flange);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Flowline_Gutter);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.TopCurb);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.BottomCurb);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.BackCurb);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.SidewalkIn);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.SidewalkOut);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.HingeCut);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.HingeFill);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Top);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Datum);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Pave);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Pave1);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Pave2);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Base);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.SubBase);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Gravel);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.TopCurbNew);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.BackCurbNew);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Curb);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Sidewalk);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Hinge);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.EOV);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.EOVOverlay);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Level);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Mill);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Overlay);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.CrownOverlay);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Barrier);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.EBD);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.CrownDeck);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Deck);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Girder);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.EBS);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.ESL);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.DaylightBallast);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.ESBS);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.DaylightSubballast);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Ballast);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Sleeper);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Subballast);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Rail);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.R1);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.R2);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.R3);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.R4);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.R5);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.R6);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Bridge);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Ditch);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.CrownFin);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.CrownSubBase);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.ETWSubBase);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.MarkedPoint);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Guardrail);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Median);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.ETWOverlay);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.TrenchBottom);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.TrenchDaylight);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.TrenchBedding);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.TrenchBackfill);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Trench);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.LaneBreak);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.LaneBreakOverlay);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Sod);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.DaylightStrip);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.sForeslopeStripping);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Stripping);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.ChannelFlowline);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Channe_Bottom);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.ChannelTop);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.ChannelExtension);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.ChannelBackslope);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.LiningMaterial);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.DitchBack);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.DitchFace);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.DitchTop);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.DitchBottom);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Backfill);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.BackfillFace);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.DitchLidFace);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.LidTop);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.DitchBackFill);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Lid);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.DrainBottom);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.DrainBottomOutside);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.DrainTopOutside);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.DrainTopInside);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.DrainBottomInside);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.DrainCenter);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.FlowLine);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.DrainTop);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.DrainStructure);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.DrainArea);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.RWFront);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.RWTop);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.RWBack);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.RWHinge);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.RWInside);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.RWOutside);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Wall);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.RWall);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.RWallB1);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.RWallB2);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.RWallB3);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.RWallB4);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.RWallK1);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.RWallK2);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.FootingBottom);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.WalkEdge);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Lot);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Slope_Link);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Channel_Side);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Bench);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.CrownPave3);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.LanePave3);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.ETWBase1);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.CrownBase1);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.LaneBase1);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.ETWBase2);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.CrownBase2);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.LaneBase2);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.ETWBase3);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.CrownBase3);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.LaneBase3);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.ETWSub1);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.CrownSub1);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.LaneSub1);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.ETWSub2);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.CrownSub2);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.LaneSub2);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.ETWSub3);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.CrownSub3);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.LaneSub3);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Pave3);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Base1);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Base2);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Base3);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Subbase1);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Subbase2);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Subbase3);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.EPSBase1);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.EPSBase2);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.EPSBase3);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.EPSSubBase1);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.EPSSubBase2);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.EPSSubBase3);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.ETWPave3);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.EPSBase4);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.Base4);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.SR);
			GetFromCollection(colCodesAndDescriptionHashtable, ref i, ref Codes.EPSPave3);
		}
		catch (object obj) when ((obj is Exception && num != 0) & (num2 == 0))
		{
			ProjectData.SetProjectError((Exception)obj);
			/*Error near IL_1431: Could not find block for branch target IL_13f9*/;
		}
		if (num2 != 0)
		{
			ProjectData.ClearProjectError();
		}
	}

	private static void GetFromCollection(Collection colCodesAndDescriptionHashtable, ref int n, ref CodeType g_sEachCode)
	{
		checked
		{
			int num = default(int);
			int num2 = default(int);
			try
			{
				ProjectData.ClearProjectError();
				num = 2;
				n++;
				g_sEachCode.Index = n;
				string sCodesAndDes = Conversions.ToString(colCodesAndDescriptionHashtable["I" + Conversions.ToString(n)]);
				int firstCommaPos = Strings.InStr(1, sCodesAndDes, ",");
				string sCode;
				string sDescription;
				if (firstCommaPos != 0)
				{
					sCode = Strings.Left(sCodesAndDes, firstCommaPos - 1);
					int SecondCommaPos = Strings.InStr(firstCommaPos + 1, sCodesAndDes, ",");
					sDescription = ((SecondCommaPos == 0) ? "" : Strings.Mid(sCodesAndDes, SecondCommaPos + 1));
				}
				else
				{
					sCode = sCodesAndDes;
					sDescription = "";
				}
				g_sEachCode.Code = sCode;
				g_sEachCode.Description = sDescription;
				if (Information.Err().Number != 0)
				{
					Debug.Print("Error for code " + Conversions.ToString(n));
				}
			}
			catch (object obj) when ((obj is Exception && num != 0) & (num2 == 0))
			{
				ProjectData.SetProjectError((Exception)obj);
				/*Error near IL_0111: Could not find block for branch target IL_00dd*/;
			}
			if (num2 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}
	}
}
