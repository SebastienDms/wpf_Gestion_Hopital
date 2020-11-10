#region Ressources extérieures

#endregion

using System;

namespace Gestion_Hopital.Model
{
    /// <summary>
    /// Classe de définition des données
    /// </summary>
    public class C_t_medecins
    {
        #region Donnees
        private int _IDMed;
        private string _NomMed;
        private string _PrenomMed;
        private int _GSMMed;
        private int _IDSpe;
        private DateTime? _DateNaisMed;
        #endregion
        #region Constructeurs
        public C_t_medecins()
        {
        }

        public C_t_medecins(string NomMed_, string PrenomMed_, int GSMMed_, int IDSpe_, DateTime? DateNaisMed_)
        {
            this.NomMed = NomMed_;
            this.PrenomMed = PrenomMed_;
            this.GSMMed = GSMMed_;
            this.IDSpe = IDSpe_;
            this.DateNaisMed = DateNaisMed_;
            //this.DateNaisMed = new DateTime?(DateNaisMed_);
        }

        public C_t_medecins(int IDMed_, string NomMed_, string PrenomMed_, int GSMMed_, int IDSpe_, DateTime? DateNaisMed_)
            : this(NomMed_, PrenomMed_, GSMMed_, IDSpe_, DateNaisMed_)
        {
            this.IDMed = IDMed_;
        }
        #endregion
        #region Accesseurs
        public int IDMed
        {
            get => this._IDMed;
            set => this._IDMed = value;
        }

        public string NomMed
        {
            get => this._NomMed;
            set => this._NomMed = value;
        }

        public string PrenomMed
        {
            get => this._PrenomMed;
            set => this._PrenomMed = value;
        }

        public int GSMMed
        {
            get => this._GSMMed;
            set => this._GSMMed = value;
        }

        public int IDSpe
        {
            get => this._IDSpe;
            set => this._IDSpe = value;
        }

        public DateTime? DateNaisMed
        {
            get => this._DateNaisMed;
            set => this._DateNaisMed = value;
        }
        #endregion
    }
}
