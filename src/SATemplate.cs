#region using statements
using System;

using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.Civil.ApplicationServices;
using Autodesk.Civil.Runtime;
#endregion

namespace C3DAS
{
    public abstract class SATemplate
    {
        public void GetLogicalNames()
        {
            Transaction trans = null;
            CorridorState corridorState = null;
            try
            {
                trans = StartTransaction();
                corridorState = CivilApplication.ActiveDocument.CorridorState;
                GetLogicalNamesImplement(corridorState);
                trans.Commit();
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                Exception e = ex;
                Utilities.RecordError(corridorState, e);
                if (trans != null)
                {
                    trans.Abort();
                }
                ProjectData.ClearProjectError();
            }
            finally
            {
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }

        public void GetInputParameters()
        {
            Transaction trans = null;
            CorridorState corridorState = null;
            try
            {
                trans = StartTransaction();
                corridorState = CivilApplication.ActiveDocument.CorridorState;
                GetInputParametersImplement(corridorState);
                trans.Commit();
            }
            catch (Exception ex)
            {
                Exception e = ex;
                Utilities.RecordError(corridorState, e);
                if (trans != null)
                {
                    trans.Abort();
                }
            }
            finally
            {
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }

        public void GetOutputParameters()
        {
            Transaction trans = null;
            CorridorState corridorState = null;
            try
            {
                trans = StartTransaction();
                GetOutputParametersImplement(corridorState);
                corridorState = CivilApplication.ActiveDocument.CorridorState;
                trans.Commit();
            }
            catch (Exception ex)
            {
                Exception e = ex;
                Utilities.RecordError(corridorState, e);
                if (trans != null)
                {
                    trans.Abort();
                }
            }
            finally
            {
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }

        public void Draw()
        {
            CorridorState corridorState = null;
            try
            {
                corridorState = CivilApplication.ActiveDocument.CorridorState;
                DrawImplement(corridorState);
            }
            catch (Exception ex)
            {
                Exception e = ex;
                Utilities.RecordError(corridorState, e);
            }
        }

        protected virtual void GetLogicalNamesImplement(CorridorState corridorState)
        {
        }

        protected virtual void GetInputParametersImplement(CorridorState corridorState)
        {
        }

        protected virtual void GetOutputParametersImplement(CorridorState corridorState)
        {
        }

        protected abstract void DrawImplement(CorridorState corridorState);

        protected Transaction StartTransaction()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            TransactionManager tm = db.TransactionManager;
            return tm.StartTransaction();
        }

        protected SATemplate()
        {
            if (!CodesSpecific.Codes.CodesStructureFilled)
            {
                CodesSpecific.FillCodeStructure();
            }
        }
    }

}
}
