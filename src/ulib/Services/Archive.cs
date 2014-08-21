using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                return null;

            IArchive arc = ArchiveUtil.CreateArchive(at);
            if (arc == null)
                return null;

            return arc.LoadFrom(targetLocation);
        }
    }
}
