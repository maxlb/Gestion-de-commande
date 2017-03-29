using Microsoft.VisualStudio.TestTools.UnitTesting;
using GestionCommande.vue;
using GestionCommande.controleur;
using GestionCommande.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace GestionCommande.tests_unitaires
{
    [TestClass]
    public class TestsUnitaires
    {
        [TestMethod]
        public void TestCreerClient()
        {
            Controleur controleur = new CommandeControleur();
            string n = "Jean";
            string p = "Pierre";
            string m = "jean.pierre@orange.fr";
            controleur.CreerClient(n, p, m);
            Assert.AreEqual(n , controleur.GetClients().Last().Nom);
            Assert.AreEqual(p, controleur.GetClients().Last().Prenom);
            Assert.AreEqual(m, controleur.GetClients().Last().Mail);
        }

        [TestMethod]
        public void TestCreerProduit()
        {
            Controleur controleur = new CommandeControleur();
            string d = "Table";
            int p = 50;
            controleur.CreerProduit(d,p);
            Assert.AreEqual(d, controleur.GetProduits().Last().Designation);
            Assert.AreEqual(p, controleur.GetProduits().Last().Prix);
        }

        [TestMethod]
        public void TestCreerCommande()
        {
            Controleur controleur = new CommandeControleur();

            ICollection<LigneCommande> lignesCommande = new Collection<LigneCommande>();
            LigneCommande l1 = new LigneCommande() { Produit = controleur.GetProduits().First(), Quantite = 2 };
            LigneCommande l2 = new LigneCommande() { Produit = controleur.GetProduits().Last(), Quantite = 3 };
            lignesCommande.Add(l1);
            lignesCommande.Add(l2);

            Client c = controleur.GetClients().First();

            controleur.CreerCommande(c, lignesCommande);

            Assert.AreEqual(c, controleur.GetCommandes().Last().Client);
            Assert.AreEqual(controleur.GetProduits().First(), controleur.GetCommandes().Last().LignesCommande.First().Produit);
            Assert.AreEqual(2, controleur.GetCommandes().Last().LignesCommande.First().Quantite);
            Assert.AreEqual(controleur.GetProduits().Last(), controleur.GetCommandes().Last().LignesCommande.Last().Produit);
            Assert.AreEqual(3, controleur.GetCommandes().Last().LignesCommande.Last().Quantite);
        }

    }
}
