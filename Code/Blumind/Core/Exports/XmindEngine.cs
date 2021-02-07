using Blumind.Model.Documents;
using Blumind.Model.MindMaps;

namespace Blumind.Core.Exports
{
    class XmindEngine: ChartsExportEngine
    {
        public override string TypeMime
        {
            get { return DocumentType.Xmind.TypeMime; }
        }

        protected override bool ExportChartToFile(Document document, ChartPage chart, string filename)
        {
            if (chart is MindMap)
            {
                XmindFile.SaveFile((MindMap)chart, filename);
                return true;
            }

            return false;
        }
    }
}
