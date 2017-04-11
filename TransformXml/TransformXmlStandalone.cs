using Microsoft.Web.XmlTransform;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TransformXml
{
    public class TransformXmlStandalone
    {
        private string _sourceFile;

        private string _transformFile;

        private string _destinationFile;

        private string _sourceRootPath = string.Empty;

        private string _transformRootPath = string.Empty;

        public string Destination
        {
            get
            {
                return this._destinationFile;
            }
            set
            {
                this._destinationFile = value;
            }
        }

        public string Source
        {
            get
            {
                return GetFilePathResolution(this._sourceFile, this.SourceRootPath);
            }
            set
            {
                this._sourceFile = value;
            }
        }

        public string SourceRootPath
        {
            get
            {
                return this._sourceRootPath;
            }
            set
            {
                this._sourceRootPath = value;
            }
        }

        public string Transform
        {
            get
            {
                return GetFilePathResolution(this._transformFile, this.TransformRootPath);
            }
            set
            {
                this._transformFile = value;
            }
        }

        public string TransformRootPath
        {
            get
            {
                if (string.IsNullOrEmpty(this._transformRootPath))
                {
                    return this.SourceRootPath;
                }
                return this._transformRootPath;
            }
            set
            {
                this._transformRootPath = value;
            }
        }

        public bool TransformXml()
        {
            bool flag = true;
            XmlTransformation xmlTransformation = null;
            XmlTransformableDocument xmlTransformableDocument = null;
            try
            {
                try
                {
                    CultureInfo currentCulture = CultureInfo.CurrentCulture;
                    string bUILDTASKTransformXmlTransformationStart = "Transforming Source File: {0}";
                    object[] source = new object[] { this.Source };
                    Console.WriteLine(string.Format(currentCulture, bUILDTASKTransformXmlTransformationStart, source));
                    xmlTransformableDocument = this.OpenSourceFile(this.Source);
                    CultureInfo cultureInfo = CultureInfo.CurrentCulture;
                    string bUILDTASKTransformXmlTransformationApply = "Applying Transform File: {0}";
                    object[] transform = new object[] { this.Transform };
                    Console.WriteLine(string.Format(cultureInfo, bUILDTASKTransformXmlTransformationApply, this.Transform));
                    xmlTransformation = this.OpenTransformFile(this.Transform);
                    flag = xmlTransformation.Apply(xmlTransformableDocument);
                    if (flag)
                    {
                        CultureInfo currentCulture1 = CultureInfo.CurrentCulture;
                        string bUILDTASKTransformXmlTransformOutput ="Output File: {0}";
                        Console.WriteLine(string.Format(currentCulture1, bUILDTASKTransformXmlTransformOutput, this.Destination));
                        SaveTransformedFile(xmlTransformableDocument, this.Destination);
                    }
                }
                catch (XmlException xmlException1)
                {
                    XmlException xmlException = xmlException1;
                    string localPath = this.Source;
                    if (!string.IsNullOrEmpty(xmlException.SourceUri))
                    {
                        localPath = (new Uri(xmlException.SourceUri)).LocalPath;
                    }
                    Console.WriteLine(string.Format("{0} {1} {2} {3} {4}", localPath, xmlException.LineNumber, xmlException.LinePosition, xmlException.Message));
                    flag = false;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    flag = false;
                }
            }
            finally
            {
                Console.WriteLine(string.Format(CultureInfo.CurrentCulture, (flag ? "Transformation succeeded" : "Transformation failed"), new object[0]), new object[0]);
                if (xmlTransformation != null)
                {
                    xmlTransformation.Dispose();
                }
                if (xmlTransformableDocument != null)
                {
                    xmlTransformableDocument.Dispose();
                }
            }
            return flag;
        }
        public string GetFilePathResolution(string source, string sourceRootPath)
        {
            if (Path.IsPathRooted(source) || string.IsNullOrEmpty(sourceRootPath))
            {
                return source;
            }
            return Path.Combine(sourceRootPath, source);
        }
        private XmlTransformableDocument OpenSourceFile(string sourceFile)
        {
            XmlTransformableDocument xmlTransformableDocument;
            try
            {
                XmlTransformableDocument xmlTransformableDocument1 = new XmlTransformableDocument()
                {
                    PreserveWhitespace = true
                };
                xmlTransformableDocument1.Load(sourceFile);
                xmlTransformableDocument = xmlTransformableDocument1;
            }
            catch (XmlException xmlException)
            {
                throw;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                CultureInfo currentCulture = CultureInfo.CurrentCulture;
                string bUILDTASKTransformXmlSourceLoadFailed = "Could not open Source file: {0}";
                object[] message = new object[] { exception.Message };
                throw new Exception(string.Format(currentCulture, bUILDTASKTransformXmlSourceLoadFailed, message), exception);
            }
            return xmlTransformableDocument;
        }

        private XmlTransformation OpenTransformFile(string transformFile)
        {
            XmlTransformation xmlTransformation;
            try
            {
                xmlTransformation = new XmlTransformation(transformFile);
            }
            catch (XmlException xmlException)
            {
                throw;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                CultureInfo currentCulture = CultureInfo.CurrentCulture;
                string bUILDTASKTransformXmlTransformLoadFailed = "Could not open Transform file: {0}";
                object[] message = new object[] { exception.Message };
                throw new Exception(string.Format(currentCulture, bUILDTASKTransformXmlTransformLoadFailed, message), exception);
            }
            return xmlTransformation;
        }

        private void SaveTransformedFile(XmlTransformableDocument document, string destinationFile)
        {
            try
            {
                document.Save(destinationFile);
            }
            catch (XmlException xmlException)
            {
                throw;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                CultureInfo currentCulture = CultureInfo.CurrentCulture;
                //string bUILDTASKTransformXmlDestinationWriteFailed = SR.BUILDTASK_TransformXml_DestinationWriteFailed;
                object[] message = new object[] { exception.Message };
                throw new Exception(string.Format(currentCulture, "", message), exception);// bUILDTASKTransformXmlDestinationWriteFailed, message), exception);
            }
        }
    }
}
