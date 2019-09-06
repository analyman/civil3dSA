using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.Civil;
using Autodesk.Civil.ApplicationServices;
using Autodesk.Civil.DatabaseServices;
using Autodesk.Civil.Runtime;
using Autodesk.Civil.Settings;
using System;

namespace C3DAS
{

    public class Utilities
    {
        public enum FoundOrNotFound
        {
            NotFoundSituation,
            FoundSituation,
            UndefinedSituation
        }

        public enum FillOrCut
        {
            FillSituation,
            CutSituation,
            UndefinedSituation
        }

        public enum ShoulderSubbaseType
        {
            Shoulder,
            Subbase
        }

        public enum RoundingOption
        {
            NoneType,
            CircularType,
            ParabolicType
        }

        public enum SideOption
        {
            Inside,
            Outside
        }

        public enum SlopeDirection
        {
            AwayFromCrown,
            TowardsCrown
        }

        public enum SuperelevationSlope
        {
            LeftInsideLane,
            LeftOutsideLane,
            LeftInsideShoulder,
            LeftOutsideShoulder,
            RightInsideLane,
            RightOutsideLane,
            RightInsideShoulder,
            RightOutsideShoulder
        }

        public enum RoundingBy
        {
            ByLength,
            ByRadius
        }

        public const string R2010 = "R2010";

        public const string R2009 = "R2009";

        public const string R2008 = "R2008";

        public const string R2007 = "R2007";

        public const string R2006 = "R2006";

        public const string R2005 = "R2005";

        public const string Side = "Side";

        public const int Left = 1;

        public const int Right = 0;

        public const int ITrue = 1;

        public const int IFalse = 0;

        public const double PI = Math.PI;

        public const double BigDistance = 1000000.0;

        public static void RecordError(CorridorState corridorState, Exception e)
        {
            if (corridorState == null)
            {
                throw new ArgumentNullException("corridorState");
            }
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            corridorState.RecordError(-2147221503, 3, e.Message, e.Source, true);
        }

        public static void RecordWarning(CorridorState corridorState, CorridorError corridorError, string message, string source)
        {
            //IL_0016: Unknown result type (might be due to invalid IL or missing references)
            if (corridorState == null)
            {
                throw new ArgumentNullException("corridorState");
            }
            corridorState.RecordError(corridorError, 2, message, source, true);
        }

        public static void RecordError(CorridorState corridorState, CorridorError corridorError, string message, string source)
        {
            //IL_0016: Unknown result type (might be due to invalid IL or missing references)
            if (corridorState == null)
            {
                throw new ArgumentNullException("corridorState");
            }
            corridorState.RecordError(corridorError, 3, message, source, true);
        }

        public static void SetSEAORUnsupportedTag(CorridorState corridorState)
        {
            if (corridorState == null)
            {
                throw new ArgumentNullException("corridorState");
            }
            IParam param = corridorState.ParamsLong.Add("SE AOR Unsupported", 1);
            if (param != null)
            {
                param.Access=ParamAccessType.Output;
            }
        }

        public static void ClearSEAORUnsupportedTag(CorridorState corridorState)
        {
            if (corridorState == null)
            {
                throw new ArgumentNullException("corridorState");
            }
            IParam param = corridorState.ParamsLong.Add("SE AOR Unsupported", 0);
            if (param != null)
            {
                param.Access=ParamAccessType.Output;
            }
        }

        public static void SetSEAORCrownPointForLayout(CorridorState corridorState, int nCrownPoint)
        {
            if (corridorState == null)
            {
                throw new ArgumentNullException("corridorState");
            }
            checked
            {
                long nCPlusPlusIndex = nCrownPoint - 1;
                IParam param = corridorState.ParamsLong.Add("SE AOR Crown Point For Layout", (int)nCPlusPlusIndex);
                if (param != null)
                {
                    param.Access=ParamAccessType.Output;
                }
            }
        }

        public static double AdjustOffset(CorridorState oRwyState, double dOffset)
        {
            //IL_0016: Unknown result type (might be due to invalid IL or missing references)
            //IL_001c: Invalid comparison between Unknown and I4
            if (oRwyState == null)
            {
                throw new ArgumentNullException("oRwyState");
            }
            if ((int)oRwyState.Mode == 1)
            {
                return dOffset;
            }
            if (oRwyState.CurrentAlignmentIsOffsetAlignment & oRwyState.CurrentAssemblyOffsetIsFixed)
            {
                return dOffset + oRwyState.CurrentAssemblyFixedOffset;
            }
            return dOffset;
        }

        public static string GetVersion(CorridorState rwState)
        {
            if (rwState == null)
            {
                throw new ArgumentNullException("rwState");
            }
            ParamStringCollection oParamsString = rwState.ParamsString;
            string vVersion;
            try
            {
                vVersion = oParamsString.Value("Version");
            }
            catch (Exception projectError)
            {
                ProjectData.SetProjectError(projectError);
                vVersion = "R2005";
                ProjectData.ClearProjectError();
            }
            return vVersion;
        }

        public static int GetVersionInt(CorridorState rwState)
        {
            string sVersion2 = GetVersion(rwState);
            sVersion2 = sVersion2.Substring(1);
            return int.Parse(sVersion2);
        }

        public static void GetAlignmentAndOrigin(CorridorState rwState, ref ObjectId oAlignmentId, ref PointInMem oOrigin)
        {
            //IL_001f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0025: Expected O, but got Unknown
            //IL_0026: Unknown result type (might be due to invalid IL or missing references)
            //IL_002c: Invalid comparison between Unknown and I4
            //IL_0090: Unknown result type (might be due to invalid IL or missing references)
            //IL_0095: Unknown result type (might be due to invalid IL or missing references)
            //IL_00ca: Unknown result type (might be due to invalid IL or missing references)
            //IL_00cf: Unknown result type (might be due to invalid IL or missing references)
            if (rwState == null)
            {
                throw new ArgumentNullException("rwState");
            }
            if (oOrigin == null)
            {
                oOrigin = new PointInMem();
            }
            if ((int)rwState.Mode == 1)
            {
                oOrigin.Station=0.0;
                oOrigin.Offset=0.0;
                oOrigin.Elevation=0.0;
                return;
            }
            bool isFixedAlignmentOffset = rwState.CurrentAlignmentIsOffsetAlignment & rwState.CurrentAssemblyOffsetIsFixed;
            oOrigin.Station=rwState.CurrentStation;
            if (isFixedAlignmentOffset)
            {
                oAlignmentId = rwState.CurrentBaselineId;
                oOrigin.Offset=rwState.CurrentOffset + rwState.CurrentAssemblyFixedOffset;
                oOrigin.Elevation=rwState.CurrentElevation + rwState.CurrentAssemblyFixedElevation;
            }
            else
            {
                oAlignmentId = rwState.CurrentAlignmentId;
                oOrigin.Offset=rwState.CurrentOffset;
                oOrigin.Elevation=rwState.CurrentElevation;
            }
        }

        public static void CalcElevationOnSurface(ObjectId surfaceId, ObjectId alignmentId, double stationOnAlignment, double offsetToAlignment, ref double elevationOnSurface)
        {
            //IL_0013: Unknown result type (might be due to invalid IL or missing references)
            //IL_001c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0022: Expected O, but got Unknown
            //IL_0024: Unknown result type (might be due to invalid IL or missing references)
            //IL_002d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0033: Expected O, but got Unknown
            Database db = HostApplicationServices.WorkingDatabase;
            TransactionManager tm = db.TransactionManager;
            Surface surface = tm.GetObject(surfaceId, 0, false, false);
            Alignment alignment = tm.GetObject(alignmentId, 0, false, false);
            double x = default(double);
            double y = default(double);
            alignment.PointLocation(stationOnAlignment, offsetToAlignment, ref x, ref y);
            elevationOnSurface = surface.FindElevationAtXY(x, y);
        }

        public static bool GetRoundingCurve(IPoint oIntersectPt, double dSlope1, double dSlope2, RoundingOption nRoundingOption, RoundingBy nRoundingBy, double dRoundingValue, long nRoundingTesselation, double dTangentLength1, double dTangentLength2, bool isRight, ref IPoint[] tesselatedPts)
        {
            //IL_0602: Unknown result type (might be due to invalid IL or missing references)
            //IL_0608: Expected O, but got Unknown
            //IL_0883: Unknown result type (might be due to invalid IL or missing references)
            //IL_0889: Expected O, but got Unknown
            int num = default(int);
            bool GetRoundingCurve;
            int num4 = default(int);
            try
            {
                ProjectData.ClearProjectError();
                num = -2;
                GetRoundingCurve = false;
                switch (nRoundingOption)
                {
                    case RoundingOption.NoneType:
                        goto end_IL_0001;
                    case RoundingOption.CircularType:
                        {
                            double dAngle6 = Math.Atan(dSlope1);
                            dAngle6 = ((dAngle6 < 0.0) ? ((!isRight) ? (Math.PI - dAngle6) : (Math.PI * 2.0 + dAngle6)) : ((dAngle6 > 0.0) ? ((!isRight) ? (Math.PI - dAngle6) : dAngle6) : ((!isRight) ? Math.PI : 0.0)));
                            dAngle6 += Math.PI;
                            if (dAngle6 > Math.PI * 2.0)
                            {
                                dAngle6 -= Math.PI * 2.0;
                            }
                            double dAngle3 = Math.Atan(dSlope2);
                            dAngle3 = ((dAngle3 < 0.0) ? ((!isRight) ? (Math.PI - dAngle3) : (Math.PI * 2.0 + dAngle3)) : ((dAngle3 > 0.0) ? ((!isRight) ? (Math.PI - dAngle3) : dAngle3) : ((!isRight) ? Math.PI : 0.0)));
                            double dArcAngle = Math.Abs(Math.PI - Math.Abs(dAngle6 - dAngle3));
                            double dRadius2 = dRoundingValue;
                            if (nRoundingBy == RoundingBy.ByLength)
                            {
                                dRadius2 = dRoundingValue / dArcAngle;
                            }
                            double dTangentLength3 = dRadius2 * Math.Tan(dArcAngle / 2.0);
                            if (dTangentLength1 < dTangentLength3)
                            {
                                dTangentLength3 = dTangentLength1;
                            }
                            if (dTangentLength2 < dTangentLength3)
                            {
                                dTangentLength3 = dTangentLength2;
                            }
                            dRadius2 = dTangentLength3 / Math.Tan(dArcAngle / 2.0);
                            double[] vIntersectPt = new double[3]
                            {
                    oIntersectPt.Offset,
                    oIntersectPt.Elevation,
                    0.0
                            };
                            double[] vStartPt = Utility.PolarPoint(vIntersectPt, dAngle6, dTangentLength3);
                            double[] vEndPt = Utility.PolarPoint(vIntersectPt, dAngle3, dTangentLength3);
                            double dIntPtAngle = (!(Math.Abs(dAngle3 - dAngle6) > Math.PI)) ? (dAngle6 + (dAngle3 - dAngle6) / 2.0) : (dAngle6 + (dAngle3 - dAngle6) / 2.0 + Math.PI);
                            if (dIntPtAngle > Math.PI * 2.0)
                            {
                                dIntPtAngle -= Math.PI * 2.0;
                            }
                            double[] vCenterPt = new double[3]
                            {
                    oIntersectPt.Offset + dRadius2 / Math.Cos(dArcAngle / 2.0) * Math.Cos(dIntPtAngle),
                    oIntersectPt.Elevation + dRadius2 / Math.Cos(dArcAngle / 2.0) * Math.Sin(dIntPtAngle),
                    0.0
                            };
                            double dStartAng;
                            if (Math.Abs(vStartPt[0] - vCenterPt[0]) < 1E-05)
                            {
                                dStartAng = ((!(vStartPt[1] < vCenterPt[1])) ? (Math.PI / 2.0) : 4.71238898038469);
                            }
                            else
                            {
                                dStartAng = Math.Atan((vStartPt[1] - vCenterPt[1]) / (vStartPt[0] - vCenterPt[0]));
                                if (!(dStartAng > 0.0))
                                {
                                    dStartAng = ((!(vStartPt[0] < vCenterPt[0])) ? (Math.PI * 2.0 + dStartAng) : (Math.PI + dStartAng));
                                }
                                else if (vStartPt[0] < vCenterPt[0])
                                {
                                    dStartAng = Math.PI + dStartAng;
                                }
                            }
                            double dEndAng;
                            if (Math.Abs(vEndPt[0] - vCenterPt[0]) < 1E-05)
                            {
                                dEndAng = ((!(vEndPt[1] < vCenterPt[1])) ? (Math.PI / 2.0) : 4.71238898038469);
                            }
                            else
                            {
                                dEndAng = Math.Atan((vEndPt[1] - vCenterPt[1]) / (vEndPt[0] - vCenterPt[0]));
                                if (!(dEndAng > 0.0))
                                {
                                    dEndAng = ((!(vEndPt[0] < vCenterPt[0])) ? (Math.PI * 2.0 + dEndAng) : (Math.PI + dEndAng));
                                }
                                else if (vEndPt[0] < vCenterPt[0])
                                {
                                    dEndAng = Math.PI + dEndAng;
                                }
                            }
                            double dDeltaAng = (dEndAng - dStartAng) / (double)checked(nRoundingTesselation + 1);
                            int num3 = checked((int)(nRoundingTesselation + 1));
                            for (int i = 0; i <= num3; i = checked(i + 1))
                            {
                                double[] vCurrentPt = Utility.PolarPoint(vCenterPt, dStartAng + (double)i * dDeltaAng, dRadius2);
                                tesselatedPts[i] = new PointInMem();
                                tesselatedPts[i].Station=oIntersectPt.Station;
                                tesselatedPts[i].Offset=vCurrentPt[0];
                                tesselatedPts[i].Elevation=vCurrentPt[1];
                            }
                            break;
                        }
                    default:
                        {
                            if (!isRight)
                            {
                                double dTempSwap2 = dSlope2;
                                dSlope2 = 0.0 - dSlope1;
                                dSlope1 = 0.0 - dTempSwap2;
                                dTempSwap2 = dTangentLength2;
                                dTangentLength2 = dTangentLength1;
                                dTangentLength1 = dTempSwap2;
                            }
                            double dL;
                            double dK;
                            if (nRoundingBy == RoundingBy.ByLength)
                            {
                                dL = dRoundingValue;
                                dK = (dSlope2 - dSlope1) / dL;
                            }
                            else
                            {
                                dK = dRoundingValue / 100.0;
                                dL = (dSlope2 - dSlope1) / dK;
                                if (dL < 0.0)
                                {
                                    dK = 0.0 - dK;
                                    dL = 0.0 - dL;
                                }
                            }
                            double dT5 = Math.Abs(dL / 2.0 / Math.Cos(Math.Atan(dSlope1)));
                            if (dTangentLength1 < dT5)
                            {
                                dT5 = dTangentLength1;
                                dL = dT5 * Math.Cos(Math.Atan(dSlope1)) * 2.0;
                            }
                            double dT3 = Math.Abs(dL / 2.0 / Math.Cos(Math.Atan(dSlope2)));
                            if (dTangentLength2 < dT3)
                            {
                                dT3 = dTangentLength2;
                                dL = dT3 * Math.Cos(Math.Atan(dSlope2)) * 2.0;
                                Math.Abs(dL / 2.0 / Math.Cos(Math.Atan(dSlope1)));
                            }
                            double dDeltaX = dL / (double)checked(nRoundingTesselation + 1);
                            int num2 = checked((int)(nRoundingTesselation + 1));
                            for (int j = 0; j <= num2; j = checked(j + 1))
                            {
                                double x = (double)j * dDeltaX;
                                int nCurrentIndex = (!isRight) ? checked((int)(nRoundingTesselation + 1 - j)) : j;
                                tesselatedPts[nCurrentIndex] = new PointInMem();
                                tesselatedPts[nCurrentIndex].Station=oIntersectPt.Station;
                                tesselatedPts[nCurrentIndex].Offset=oIntersectPt.Offset + (x - dL / 2.0);
                                if (x < dL / 2.0)
                                {
                                    tesselatedPts[nCurrentIndex].Elevation=oIntersectPt.Elevation - (dL / 2.0 - x * dSlope1 + dK / 2.0 * x * x);
                                }
                                else
                                {
                                    tesselatedPts[nCurrentIndex].Elevation=oIntersectPt.Elevation + (x - dL / 2.0 * dSlope2 + dK / 2.0 * (dL * dL + x * x) - dL * dK * x);
                                }
                            }
                            break;
                        }
                }
                GetRoundingCurve = true;
            }
            catch (object obj) when ((obj is Exception && num != 0) & (num4 == 0))
            {
                ProjectData.SetProjectError((Exception)obj);
                /*Error near IL_0ca2: Could not find block for branch target IL_0c6a*/
                ;
            }
            if (num4 != 0)
            {
                ProjectData.ClearProjectError();
            }
            return GetRoundingCurve;
        }

        public static AlignmentSide GetSide(int vSide)
        {
            //IL_000a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0016: Unknown result type (might be due to invalid IL or missing references)
            //IL_001b: Unknown result type (might be due to invalid IL or missing references)
            //IL_001e: Unknown result type (might be due to invalid IL or missing references)
            switch (vSide)
            {
                case 1:
                    return 1;
                case 0:
                    return 2;
                default:
                    return 0;
            }
        }

        public static void AddCodeToLink(int i, LinkCollection iLinks, long linkIndex, string[,] strArrCode)
        {
            if (iLinks == null)
            {
                throw new ArgumentNullException("iLinks");
            }
            if ((i < 0) | (i > Information.UBound(strArrCode)))
            {
                throw new ArgumentOutOfRangeException("i");
            }
            checked
            {
                for (int j = 0; j <= Information.UBound(strArrCode, 2) && Operators.CompareString(strArrCode[i, j], "", TextCompare: false) != 0; j++)
                {
                    iLinks.get_Item((int)linkIndex).Codes.TryAdd(strArrCode[i, j]);
                }
            }
        }

        public static void AddCodeToPoint(int i, PointCollection iPoints, long pointIndex, string[,] strArrCode)
        {
            if (iPoints == null)
            {
                throw new ArgumentNullException("iPoints");
            }
            if ((i < 0) | (i > Information.UBound(strArrCode)))
            {
                throw new ArgumentOutOfRangeException("i");
            }
            checked
            {
                for (int j = 0; j <= Information.UBound(strArrCode, 2) && Operators.CompareString(strArrCode[i, j], "", TextCompare: false) != 0; j++)
                {
                    iPoints.get_Item((int)pointIndex).Codes.TryAdd(strArrCode[i, j]);
                }
            }
        }

        public static bool IsProjectUnitsFeet()
        {
            //IL_001b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0022: Invalid comparison between Unknown and I4
            SettingsRoot settings = CivilApplication.ActiveDocument.Settings;
            SettingsDrawing drawingSettings = settings.DrawingSettings;
            SettingsUnitZone unitZone = drawingSettings.UnitZoneSettings;
            if ((int)unitZone.DrawingUnits == 30)
            {
                return true;
            }
            return false;
        }

        public static double GetProjectUnitsDivisor()
        {
            if (IsProjectUnitsFeet())
            {
                return 12.0;
            }
            return 1000.0;
        }

        public static double GetSlope(string strShoulderSlope, CorridorState rwState, ShoulderSubbaseType shoulderSubbaseType, bool blnInsertLeft)
        {
            //IL_0050: Unknown result type (might be due to invalid IL or missing references)
            //IL_0055: Unknown result type (might be due to invalid IL or missing references)
            //IL_005b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0060: Unknown result type (might be due to invalid IL or missing references)
            //IL_006c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0074: Unknown result type (might be due to invalid IL or missing references)
            //IL_007a: Expected O, but got Unknown
            if (rwState == null)
            {
                throw new ArgumentNullException("rwState");
            }
            if (Versioned.IsNumeric(strShoulderSlope))
            {
                return Conversion.Val(strShoulderSlope) / 100.0;
            }
            ObjectId alignmentId = (!(rwState.CurrentAlignmentIsOffsetAlignment & rwState.CurrentAssemblyOffsetIsFixed)) ? rwState.CurrentAlignmentId : rwState.CurrentBaselineId;
            Alignment alignment = HostApplicationServices.WorkingDatabase.TransactionManager.GetObject(alignmentId, 0, false);
            string sTempSlope = Strings.UCase(strShoulderSlope);
            switch (shoulderSubbaseType)
            {
                case ShoulderSubbaseType.Shoulder:
                    if (blnInsertLeft)
                    {
                        if (Operators.CompareString(sTempSlope, "SI", TextCompare: false) == 0)
                        {
                            return alignment.GetCrossSlopeAtStation(rwState.CurrentStation, 2, true);
                        }
                        if (Operators.CompareString(sTempSlope, "SO", TextCompare: false) == 0)
                        {
                            return alignment.GetCrossSlopeAtStation(rwState.CurrentStation, 0, true);
                        }
                        if (Operators.CompareString(sTempSlope, "LI", TextCompare: false) == 0)
                        {
                            return alignment.GetCrossSlopeAtStation(rwState.CurrentStation, 3, true);
                        }
                        if (Operators.CompareString(sTempSlope, "LO", TextCompare: false) == 0)
                        {
                            return alignment.GetCrossSlopeAtStation(rwState.CurrentStation, 1, true);
                        }
                    }
                    else
                    {
                        if (Operators.CompareString(sTempSlope, "SI", TextCompare: false) == 0)
                        {
                            return alignment.GetCrossSlopeAtStation(rwState.CurrentStation, 258, true);
                        }
                        if (Operators.CompareString(sTempSlope, "SO", TextCompare: false) == 0)
                        {
                            return alignment.GetCrossSlopeAtStation(rwState.CurrentStation, 256, true);
                        }
                        if (Operators.CompareString(sTempSlope, "LI", TextCompare: false) == 0)
                        {
                            return alignment.GetCrossSlopeAtStation(rwState.CurrentStation, 259, true);
                        }
                        if (Operators.CompareString(sTempSlope, "LO", TextCompare: false) == 0)
                        {
                            return alignment.GetCrossSlopeAtStation(rwState.CurrentStation, 257, true);
                        }
                    }
                    break;
                case ShoulderSubbaseType.Subbase:
                    if (blnInsertLeft)
                    {
                        if (Operators.CompareString(sTempSlope, "SI", TextCompare: false) == 0)
                        {
                            return alignment.GetCrossSlopeAtStation(rwState.CurrentStation, 2, true);
                        }
                        if (Operators.CompareString(sTempSlope, "SO", TextCompare: false) == 0)
                        {
                            return alignment.GetCrossSlopeAtStation(rwState.CurrentStation, 0, true);
                        }
                        if (Operators.CompareString(sTempSlope, "LI", TextCompare: false) == 0)
                        {
                            return alignment.GetCrossSlopeAtStation(rwState.CurrentStation, 3, true);
                        }
                        if (Operators.CompareString(sTempSlope, "LO", TextCompare: false) == 0)
                        {
                            return alignment.GetCrossSlopeAtStation(rwState.CurrentStation, 1, true);
                        }
                    }
                    else
                    {
                        if (Operators.CompareString(sTempSlope, "SI", TextCompare: false) == 0)
                        {
                            return alignment.GetCrossSlopeAtStation(rwState.CurrentStation, 258, true);
                        }
                        if (Operators.CompareString(sTempSlope, "SO", TextCompare: false) == 0)
                        {
                            return alignment.GetCrossSlopeAtStation(rwState.CurrentStation, 256, true);
                        }
                        if (Operators.CompareString(sTempSlope, "LI", TextCompare: false) == 0)
                        {
                            return alignment.GetCrossSlopeAtStation(rwState.CurrentStation, 259, true);
                        }
                        if (Operators.CompareString(sTempSlope, "LO", TextCompare: false) == 0)
                        {
                            return alignment.GetCrossSlopeAtStation(rwState.CurrentStation, 257, true);
                        }
                    }
                    break;
            }
            double GetSlope = default(double);
            return GetSlope;
        }

        public static void AddPoints(int size, PointCollection points, Point[] pointArray, double[] dXArray, double[] dYArray, string[] sCodeArray)
        {
            if (pointArray == null)
            {
                throw new ArgumentNullException("pointArray");
            }
            if (points == null)
            {
                throw new ArgumentNullException("points");
            }
            for (int i = 1; i <= size; i = checked(i + 1))
            {
                pointArray[i] = points.Add(dXArray[i], dYArray[i], sCodeArray[i]);
            }
        }

        public static ParamPoint GetMarkedPoint(string strMarkedPointName, CorridorState corridorState)
        {
            if (corridorState == null)
            {
                throw new ArgumentNullException("corridorState");
            }
            ParamPointCollection paramsPoint = corridorState.ParamsPoint;
            ParamPoint GetMarkedPoint = null;
            if (paramsPoint != null)
            {
                GetMarkedPoint = paramsPoint.get_Item(Strings.UCase(strMarkedPointName));
            }
            return GetMarkedPoint;
        }

        public static bool CalcAlignmentOffsetToThisAlignment(ObjectId thisAlignmentId, double stationOnThisAlignment, ref WidthOffsetTarget offsetTarget, ref double offsetToThisAlignment, ref double xOnTarget, ref double yOnTarget)
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            return CalcAlignmentOffsetToThisAlignment(thisAlignmentId, stationOnThisAlignment, ref offsetTarget, 0, ref offsetToThisAlignment, ref xOnTarget, ref yOnTarget);
        }

        public static bool CalcAlignmentOffsetToThisAlignment(ObjectId thisAlignmentId, double stationOnThisAlignment, ref WidthOffsetTarget offsetTarget, AlignmentSide enumSide, ref double offsetToThisAlignment, ref double xOnTarget, ref double yOnTarget)
        {
            //IL_0010: Unknown result type (might be due to invalid IL or missing references)
            //IL_0019: Unknown result type (might be due to invalid IL or missing references)
            //IL_001f: Expected O, but got Unknown
            //IL_0023: Unknown result type (might be due to invalid IL or missing references)
            //IL_0025: Unknown result type (might be due to invalid IL or missing references)
            Database db = HostApplicationServices.WorkingDatabase;
            TransactionManager tm = db.TransactionManager;
            bool retVal;
            try
            {
                tm.GetObject(thisAlignmentId, 0, false, false);
                offsetToThisAlignment = offsetTarget.GetDistanceToAlignment(thisAlignmentId, stationOnThisAlignment, enumSide, ref xOnTarget, ref yOnTarget);
                retVal = true;
            }
            catch (Exception projectError)
            {
                ProjectData.SetProjectError(projectError);
                retVal = false;
                ProjectData.ClearProjectError();
            }
            return retVal;
        }

        public static Point2d GetIntersectionOf_Point1Slope1_Point2Slope2(double Point1X, double Point1Y, double Slope1, double vFlip, double Point2X, double Point2Y, double Slope2, double Distance)
        {
            //IL_003e: Unknown result type (might be due to invalid IL or missing references)
            //IL_003f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0040: Unknown result type (might be due to invalid IL or missing references)
            //IL_0047: Expected O, but got Unknown
            //IL_0047: Unknown result type (might be due to invalid IL or missing references)
            //IL_0048: Unknown result type (might be due to invalid IL or missing references)
            //IL_004a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0051: Expected O, but got Unknown
            //IL_01c2: Unknown result type (might be due to invalid IL or missing references)
            //IL_01c7: Unknown result type (might be due to invalid IL or missing references)
            //IL_01cd: Unknown result type (might be due to invalid IL or missing references)
            //IL_01d8: Unknown result type (might be due to invalid IL or missing references)
            //IL_01e0: Unknown result type (might be due to invalid IL or missing references)
            Point2d Point = default(Point2d);
            Point..ctor(Point1X, Point1Y);
            Point2d Point2 = default(Point2d);
            Point2..ctor(Point1X + vFlip * Distance, Point1Y + Distance * Slope1);
            Point2d Point3 = default(Point2d);
            Point3..ctor(Point2X, Point2Y);
            Point2d Point4 = default(Point2d);
            Point4..ctor(Point2X + vFlip * Distance, Point2Y + Distance * Slope2);
            Line2d Line = new Line2d(Point, Point2);
            Line2d Line2 = new Line2d(Point3, Point4);
            Point2d[] PointArray = Line.IntersectWith(Line2);
            double dP1P2XMax = Math.Max(Point.X, Point2.X);
            double dP1P2YMax = Math.Max(Point.Y, Point2.Y);
            double dP3P4XMax = Math.Max(Point3.X, Point4.X);
            double dP3P4YMax = Math.Max(Point3.Y, Point4.Y);
            double dP1P2XMin = Math.Min(Point.X, Point2.X);
            double dP1P2YMin = Math.Min(Point.Y, Point2.Y);
            double dP3P4XMin = Math.Min(Point3.X, Point4.X);
            double dP3P4YMin = Math.Min(Point3.Y, Point4.Y);
            Point2d GetIntersectionOf_Point1Slope1_Point2Slope2;
            if (PointArray != null)
            {
                if ((PointArray[0].X >= dP1P2XMin) & (PointArray[0].X <= dP1P2XMax) & (PointArray[0].Y >= dP1P2YMin) & (PointArray[0].Y <= dP1P2YMax) & (PointArray[0].X >= dP3P4XMin) & (PointArray[0].X <= dP3P4XMax) & (PointArray[0].Y >= dP3P4YMin) & (PointArray[0].Y <= dP3P4YMax))
                {
                    return PointArray[0];
                }
                GetIntersectionOf_Point1Slope1_Point2Slope2 = default(Point2d);
            }
            else
            {
                GetIntersectionOf_Point1Slope1_Point2Slope2 = default(Point2d);
            }
            return GetIntersectionOf_Point1Slope1_Point2Slope2;
        }
    }
}