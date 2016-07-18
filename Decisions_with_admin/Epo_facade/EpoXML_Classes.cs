using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Decisions_with_admin.Epo_facade
{
    #region GSP classes  - epo search results
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class GSP : IDisposable
    {
        #region IDisoposable implementation
        private bool disposed = false;
        
        public void Dispose()
        {
            _dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void _dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                }
                // Free your own state (unmanaged objects).
                // Set large fields to null.                
                qField = null;
                pARAMField = null;
                rESField = null;
                disposed = true;
            }
        }

        // Use C# destructor syntax for finalization code.
        ~GSP()
        {
            // Simply call Dispose(false).
            _dispose(false);
        }
        #endregion

        private decimal tmField;

        private string qField;

        private GSPPARAM[] pARAMField;

        private GSPRES rESField;

        private decimal vERField;

        /// <remarks/>
        public decimal TM
        {
            get
            {
                return this.tmField;
            }
            set
            {
                this.tmField = value;
            }
        }

        /// <remarks/>
        public string Q
        {
            get
            {
                return this.qField;
            }
            set
            {
                this.qField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PARAM")]
        public GSPPARAM[] PARAM
        {
            get
            {
                return this.pARAMField;
            }
            set
            {
                this.pARAMField = value;
            }
        }

        /// <remarks/>
        public GSPRES RES
        {
            get
            {
                return this.rESField;
            }
            set
            {
                this.rESField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal VER
        {
            get
            {
                return this.vERField;
            }
            set
            {
                this.vERField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class GSPPARAM
    {

        private string nameField;

        private string valueField;

        private string original_valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string original_value
        {
            get
            {
                return this.original_valueField;
            }
            set
            {
                this.original_valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class GSPRES
    {

        private ushort mField;

        private GSPRESNB nbField;

        private GSPRESR[] rField;

        private byte snField;

        private byte enField;

        /// <remarks/>
        public ushort M
        {
            get
            {
                return this.mField;
            }
            set
            {
                this.mField = value;
            }
        }

        /// <remarks/>
        public GSPRESNB NB
        {
            get
            {
                return this.nbField;
            }
            set
            {
                this.nbField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("R")]
        public GSPRESR[] R
        {
            get
            {
                return this.rField;
            }
            set
            {
                this.rField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte SN
        {
            get
            {
                return this.snField;
            }
            set
            {
                this.snField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte EN
        {
            get
            {
                return this.enField;
            }
            set
            {
                this.enField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class GSPRESNB
    {

        private string nuField;

        /// <remarks/>
        public string NU
        {
            get
            {
                return this.nuField;
            }
            set
            {
                this.nuField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class GSPRESR
    {

        private string uField;

        private string ueField;

        private string tField;

        private byte rkField;

        private string cRAWLDATEField;

        private string eNT_SOURCEField;

        private GSPRESRFS fsField;

        private GSPRESRMT[] mtField;

        private string sField;

        private string lANGField;

        private GSPRESRHAS hASField;

        private byte nField;

        /// <remarks/>
        public string U
        {
            get
            {
                return this.uField;
            }
            set
            {
                this.uField = value;
            }
        }

        /// <remarks/>
        public string UE
        {
            get
            {
                return this.ueField;
            }
            set
            {
                this.ueField = value;
            }
        }

        /// <remarks/>
        public string T
        {
            get
            {
                return this.tField;
            }
            set
            {
                this.tField = value;
            }
        }

        /// <remarks/>
        public byte RK
        {
            get
            {
                return this.rkField;
            }
            set
            {
                this.rkField = value;
            }
        }

        /// <remarks/>
        public string CRAWLDATE
        {
            get
            {
                return this.cRAWLDATEField;
            }
            set
            {
                this.cRAWLDATEField = value;
            }
        }

        /// <remarks/>
        public string ENT_SOURCE
        {
            get
            {
                return this.eNT_SOURCEField;
            }
            set
            {
                this.eNT_SOURCEField = value;
            }
        }

        /// <remarks/>
        public GSPRESRFS FS
        {
            get
            {
                return this.fsField;
            }
            set
            {
                this.fsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("MT")]
        public GSPRESRMT[] MT
        {
            get
            {
                return this.mtField;
            }
            set
            {
                this.mtField = value;
            }
        }

        /// <remarks/>
        public string S
        {
            get
            {
                return this.sField;
            }
            set
            {
                this.sField = value;
            }
        }

        /// <remarks/>
        public string LANG
        {
            get
            {
                return this.lANGField;
            }
            set
            {
                this.lANGField = value;
            }
        }

        /// <remarks/>
        public GSPRESRHAS HAS
        {
            get
            {
                return this.hASField;
            }
            set
            {
                this.hASField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte N
        {
            get
            {
                return this.nField;
            }
            set
            {
                this.nField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class GSPRESRFS
    {

        private string nAMEField;

        private System.DateTime vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NAME
        {
            get
            {
                return this.nAMEField;
            }
            set
            {
                this.nAMEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
        public System.DateTime VALUE
        {
            get
            {
                return this.vALUEField;
            }
            set
            {
                this.vALUEField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class GSPRESRMT
    {

        private string nField;

        private string vField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string N
        {
            get
            {
                return this.nField;
            }
            set
            {
                this.nField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string V
        {
            get
            {
                return this.vField;
            }
            set
            {
                this.vField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class GSPRESRHAS
    {

        private object lField;

        private GSPRESRHASC cField;

        /// <remarks/>
        public object L
        {
            get
            {
                return this.lField;
            }
            set
            {
                this.lField = value;
            }
        }

        /// <remarks/>
        public GSPRESRHASC C
        {
            get
            {
                return this.cField;
            }
            set
            {
                this.cField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class GSPRESRHASC
    {

        private string szField;

        private string cIDField;

        private string eNCField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SZ
        {
            get
            {
                return this.szField;
            }
            set
            {
                this.szField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CID
        {
            get
            {
                return this.cIDField;
            }
            set
            {
                this.cIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ENC
        {
            get
            {
                return this.eNCField;
            }
            set
            {
                this.eNCField = value;
            }
        }
    }
    #endregion
}