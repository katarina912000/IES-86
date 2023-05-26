using FTN.Common;
using FTN.ServiceContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NMSClient
{
    public class TestGda : IDisposable
    {

        private ModelResourcesDesc modelResourcesDesc = new ModelResourcesDesc();

        private NetworkModelGDAProxy gdaQueryProxy = null;
        private NetworkModelGDAProxy GdaQueryProxy
        {
            get
            {
                if (gdaQueryProxy != null)
                {
                    gdaQueryProxy.Abort();
                    gdaQueryProxy = null;
                }

                gdaQueryProxy = new NetworkModelGDAProxy("NetworkModelGDAEndpoint");
                gdaQueryProxy.Open();

                return gdaQueryProxy;
            }
        }

        public TestGda()
        {
        }

        #region GDAQueryService

        public string GetValues(long globalId,List<ModelCode> modelCodes)
        {
            string message = "Getting values method started.";
            Console.WriteLine(message);
            CommonTrace.WriteTrace(CommonTrace.TraceError, message);

            XmlTextWriter xmlWriter = null;
            ResourceDescription rd = null;
            string xmlString="";
            try
            {

                rd = GdaQueryProxy.GetValues(globalId, modelCodes);
               
                using (StringWriter stringWriter = new StringWriter())
                {
                    xmlWriter = new XmlTextWriter(stringWriter);
                    xmlWriter.Formatting = Formatting.Indented;
                    rd.ExportToXml(xmlWriter);
                    xmlWriter.Flush();
                    xmlString= stringWriter.ToString();

                    message = "Getting values method successfully finished.";
                    Console.WriteLine(message);
                    CommonTrace.WriteTrace(CommonTrace.TraceError, message);
                }
            }
            catch (Exception e)
            {
                message = string.Format("Getting values method for entered id = {0} failed.\n\t{1}", globalId, e.Message);
                Console.WriteLine(message);
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);
            }
            finally
            {
                if (xmlWriter != null)
                {
                    xmlWriter.Close();
                }
            }

            return xmlString;
        }

        public string GetExtentValues(DMSType type,List<ModelCode> properties)
        {
            string message = "Getting extent values method started.";
            Console.WriteLine(message);
            CommonTrace.WriteTrace(CommonTrace.TraceError, message);

            XmlTextWriter xmlWriter = null;
            int iteratorId = 0;
            List<long> ids = new List<long>();
            string xmlString = "";
            try
            {
                int numberOfResources = 2;
                int resourcesLeft = 0;


                iteratorId = GdaQueryProxy.GetExtentValues(modelResourcesDesc.GetModelCodeFromType(type), properties);
                resourcesLeft = GdaQueryProxy.IteratorResourcesLeft(iteratorId);


                //xmlWriter = new XmlTextWriter(Config.Instance.ResultDirecotry + "\\GetExtentValues_Results.xml", Encoding.Unicode);
                using (StringWriter stringWriter = new StringWriter())
                {
                    xmlWriter = new XmlTextWriter(stringWriter);
                    xmlWriter.Formatting = Formatting.Indented;

                    while (resourcesLeft > 0)
                    {
                        List<ResourceDescription> rds = GdaQueryProxy.IteratorNext(numberOfResources, iteratorId);

                        for (int i = 0; i < rds.Count; i++)
                        {
                            ids.Add(rds[i].Id);
                            rds[i].ExportToXml(xmlWriter);
                            xmlWriter.Flush();
                        }

                        resourcesLeft = GdaQueryProxy.IteratorResourcesLeft(iteratorId);
                    }

                    GdaQueryProxy.IteratorClose(iteratorId);
                    xmlString= stringWriter.ToString();
                    message = "Getting extent values method successfully finished.";
                    Console.WriteLine(message);
                    CommonTrace.WriteTrace(CommonTrace.TraceError, message);
                }
            }
            catch (Exception e)
            {
                message = string.Format("Getting extent values method failed for {0}.\n\t{1}", type, e.Message);
                Console.WriteLine(message);
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);
            }
            finally
            {
                if (xmlWriter != null)
                {
                    xmlWriter.Close();
                }
            }

            return xmlString;
        }

        public string GetRelatedValues(long sourceGlobalId,List<ModelCode> properties, ModelCode referenca,DMSType tip)
        {
            string message = "Getting related values method started.";
            Console.WriteLine(message);
            CommonTrace.WriteTrace(CommonTrace.TraceError, message);

            List<long> resultIds = new List<long>();


            XmlTextWriter xmlWriter = null;
            int numberOfResources = 2;
            string xmlString="";
            Association association;
            try
            {
                association = new Association(referenca, modelResourcesDesc.GetModelCodeFromType(tip));
                int iteratorId = GdaQueryProxy.GetRelatedValues(sourceGlobalId, properties, association);
                int resourcesLeft = GdaQueryProxy.IteratorResourcesLeft(iteratorId);
                
                //xmlWriter = new XmlTextWriter(Config.Instance.ResultDirecotry + "\\GetRelatedValues_Results.xml", Encoding.Unicode);
                using (StringWriter stringWriter = new StringWriter())
                {
                    xmlWriter = new XmlTextWriter(stringWriter);
                    xmlWriter.Formatting = Formatting.Indented;

                    while (resourcesLeft > 0)
                    {
                        List<ResourceDescription> rds = GdaQueryProxy.IteratorNext(numberOfResources, iteratorId);

                        for (int i = 0; i < rds.Count; i++)
                        {
                            resultIds.Add(rds[i].Id);
                            rds[i].ExportToXml(xmlWriter);
                            xmlWriter.Flush();
                        }

                        resourcesLeft = GdaQueryProxy.IteratorResourcesLeft(iteratorId);
                    }

                    GdaQueryProxy.IteratorClose(iteratorId);
                    xmlString = stringWriter.ToString();
                    message = "Getting related values method successfully finished.";
                    Console.WriteLine(message);
                    CommonTrace.WriteTrace(CommonTrace.TraceError, message);
                }
            }
            catch (Exception e)
            {
                message = string.Format("Getting related values method  failed for sourceGlobalId = {0} and association. Reason: {1}", sourceGlobalId, e.Message);
                Console.WriteLine(message);
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);
            }
            finally
            {
                if (xmlWriter != null)
                {
                    xmlWriter.Close();
                }
            }

            return xmlString;
        }

        public List<long> GetAllGIDS()
        {
            string message = "Getting all gids method started.";
            Console.WriteLine(message);
            CommonTrace.WriteTrace(CommonTrace.TraceError, message);

            int iteratorId = 0;
            List<long> ids = new List<long>();

            HashSet<DMSType> types = modelResourcesDesc.AllDMSTypes;
            types.Remove(DMSType.MASK_TYPE);

            try
            {
                int numberOfResources = 2;
                int resourcesLeft = 0;
                foreach(DMSType t in types)
                {
                    iteratorId = GdaQueryProxy.GetExtentValues(modelResourcesDesc.GetModelCodeFromType(t), new List<ModelCode>());
                    resourcesLeft = GdaQueryProxy.IteratorResourcesLeft(iteratorId);


                    while (resourcesLeft > 0)
                    {
                        List<ResourceDescription> rds = GdaQueryProxy.IteratorNext(numberOfResources, iteratorId);

                        for (int i = 0; i < rds.Count; i++)
                        {
                            ids.Add(rds[i].Id);
                        }

                        resourcesLeft = GdaQueryProxy.IteratorResourcesLeft(iteratorId);
                    }

                    GdaQueryProxy.IteratorClose(iteratorId);
                }
                

               

                message = "Getting all gids method successfully finished.";
                Console.WriteLine(message);
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);

            }
            catch (Exception e)
            {
                message = string.Format("Getting all gids failed.\n\t{0}", e.Message);
                Console.WriteLine(message);
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);
            }
            

            return ids;
        }

        public List<ModelCode> GetModelCodesForGID(long gid)
        {
            ModelCode mc=modelResourcesDesc.GetModelCodeFromId(gid);
            List<ModelCode> modelCodes;
            modelCodes = modelResourcesDesc.GetAllPropertyIds(mc);
            return modelCodes;
        }

        public List<DMSType> GetALLDMSTypes()
        {
            List<DMSType> allDMSTypes = new List<DMSType>();
            HashSet<DMSType> modelTypes = modelResourcesDesc.AllDMSTypes;
            foreach (DMSType type in modelTypes)
            {
                allDMSTypes.Add(type);
            }
            return allDMSTypes;
        }

        public List<ModelCode> GetModelCodesForDMSType(DMSType type)
        {
            ModelCode modelCode = modelResourcesDesc.GetModelCodeFromType(type);
            List<ModelCode> modelCodes = modelResourcesDesc.GetAllPropertyIds(modelCode);
            return modelCodes;
        }

        public List<ModelCode> GetReferences(long gid)
        {
            ModelCode mc = modelResourcesDesc.GetModelCodeFromId(gid);
            DMSType dMSType = ModelResourcesDesc.GetTypeFromModelCode(mc);
            List<ModelCode> properties_ref = modelResourcesDesc.GetPropertyIds(dMSType,PropertyType.Reference);
            List<ModelCode> properties_refs = modelResourcesDesc.GetPropertyIds(dMSType,PropertyType.ReferenceVector);
            foreach(ModelCode model in properties_ref)
            {
                properties_refs.Add(model);
            }
            return properties_refs;
        }

        #endregion GDAQueryService


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
