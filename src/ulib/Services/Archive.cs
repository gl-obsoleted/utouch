using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ucore;
using ulib.Base;
using ulib.Elements;

namespace ulib
{
    public enum ArchiveType
    {
        None = Constants.INVALID_ID,
        Json = 0,
        Num,
    }

    public interface IArchive
    {
        ArchiveType GetArcType();

        bool Validate(string targetLocation);

        Node LoadFrom(string targetLocation);
        bool SaveTo(Node node, string targetLocation);
    }

    public class ArchiveSystem
    {
        public bool Save(Node node, string targetLocation)
        {
            ArchiveType at = ArchiveUtil.FindCompatibleArchiveType(targetLocation);
            if (at == ArchiveType.None)
                return false;

            IArchive arc = ArchiveUtil.CreateArchive(at);
            if (arc == null)
                return false;

            return arc.SaveTo(node, targetLocation);
        }

        public Node Load(string targetLocation)
        {
            ArchiveType at = ArchiveUtil.FindCompatibleArchiveType(targetLocation);
            if (at == ArchiveType.None)
            {
                Logging.Instance.Log("No compatible archive found.");
                return null;
            }

            Logging.Instance.Log("Creating archive '{0}'", at);
            IArchive arc = ArchiveUtil.CreateArchive(at);
            if (arc == null)
            {
                Logging.Instance.Log("CreateArchive() failed.");
                return null;
            }

            return arc.LoadFrom(targetLocation);
        }
    }
}
