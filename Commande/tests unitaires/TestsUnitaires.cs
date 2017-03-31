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
        Controleur c;

        [TestInitialize]
        public void initialize()
        {
            c = new CommandeControleur();
        }

        [TestMethod]
        public void TestCreerClient()
        {
            c.CreerClient("Jean", "Pierre", "jean.pierre@orange.fr");
            Assert.AreEqual("Jean", c.GetClients().Last().Nom);
            Assert.AreEqual("Pierre", c.GetClients().Last().Prenom);
            Assert.AreEqual("jean.pierre@orange.fr", c.GetClients().Last().Mail);
        }

        [TestMethod]
        public void TestCreerProduit()
        {
            c.CreerProduit("Table", 50);
            Assert.AreEqual("Table", c.GetProduits().Last().Designation);
            Assert.AreEqual(50, c.GetProduits().Last().Prix);
        }

        [TestMethod]
        public void TestCreerCommande()
        {
            ICollection<LigneCommande> lignesCommande = new Collection<LigneCommande>();
            LigneCommande l1 = new LigneCommande() { Produit = c.GetProduits().First(), Quantite = 2 };
            LigneCommande l2 = new LigneCommande() { Produit = c.GetProduits().Last(), Quantite = 3 };
            lignesCommande.Add(l1);
            lignesCommande.Add(l2);
            Client cl = c.GetClients().First();
            c.CreerCommande(cl, lignesCommande);
            Assert.AreEqual(cl, c.GetCommandes().Last().Client);
            Assert.AreEqual(c.GetProduits().First(), c.GetCommandes().Last().LignesCommande.First().Produit);
            Assert.AreEqual(2, c.GetCommandes().Last().LignesCommande.First().Quantite);
            Assert.AreEqual(c.GetProduits().Last(), c.GetCommandes().Last().LignesCommande.Last().Produit);
            Assert.AreEqual(3, c.GetCommandes().Last().LignesCommande.Last().Quantite);
        }

    }
}
