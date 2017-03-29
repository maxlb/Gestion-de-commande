using GestionCommande.controleur;
using GestionCommande.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCommande.vue
{
    class MainVue
    {
        private Controleur controleur;
        public MainVue(Controleur controleur)
        {
            this.controleur = controleur;
        }

        public void start()
        {
            string input = "";
            do
            {
                Console.WriteLine("Quelle actions souhaitez vous faire : ");
                Console.WriteLine("1 : Afficher la liste des clients");
                Console.WriteLine("2 : Afficher la liste des produits");
                Console.WriteLine("3 : Afficher la liste des commandes");
                Console.WriteLine("4 : Afficher les commandes d'un client");
                Console.WriteLine("5 : Ajouter une commande");
                Console.WriteLine("6 : Ajouter un client");
                Console.WriteLine("7 : Ajouter un produit");
                Console.WriteLine("q : quitter");

                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AfficherClients();
                        break;
                    case "2":
                        AfficherProduits();
                        break;
                    case "3":
                        AfficherCommandes();
                        break;
                    case "4":
                        AfficherCommandeParClient();
                        break;
                    case "5":
                        AjouterCommande();
                        break;
                    case "6":
                        AjouterNouveauClient();
                        break;
                    case "7":
                        AjouterNouveauProduit();
                        break;
                    default:
                        break;
                }
                Console.WriteLine();
            } while (!"q".Equals(input));
        }

        private void AfficherCommandeParClient()
        {
            Client client = ChoisirClient();
            AfficherCommandes(client.Commandes);
        }

        private void AfficherCommandes()
        {
            this.AfficherCommandes(controleur.GetCommandes());
        }

        private void AfficherCommandes(ICollection<Commande> commandes)
        {
            foreach (Commande commande in commandes)
            {
                Console.WriteLine("Client : " + commande.Client.Prenom + " " + commande.Client.Nom);
                Console.WriteLine("Produits : ");
                foreach (LigneCommande ligneCommande in commande.LignesCommande)
                {
                    Console.WriteLine("Désignation : " + ligneCommande.Produit.Designation);
                    Console.WriteLine("Quantité : " + ligneCommande.Quantite);
                }
                Console.WriteLine();
            }
        }

        private void AfficherProduits()
        {
           foreach(Produit produit in controleur.GetProduits())
            {
                Console.WriteLine("Désignation : " + produit.Designation);
                Console.WriteLine("Prix : "+produit.Prix);
            }
        }

        private void AjouterCommande()
        {
            Console.WriteLine("Création d'une nouvelle commande");

            Client cli = this.ChoisirClient();
            ICollection<LigneCommande> lignesCommande = this.AjouterLignes();

            controleur.CreerCommande(cli, lignesCommande);
        }

        private Client ChoisirClient()
        {
            Console.WriteLine("Entrez l'ID du client pour la commande : ");
            ICollection<Client> clients = controleur.GetClients();
            this.AfficherClients();
            string input = Console.ReadLine();
            return  clients.Where(c => c.Id == int.Parse(input)).First();
        }

        private ICollection<LigneCommande> AjouterLignes()
        {
            ICollection<LigneCommande> lignesCommande = new Collection<LigneCommande>();
            string input = "";
            do
            {
                Produit prod = this.AjouterProduit();
                Console.WriteLine("Veuillez entrer une quantité");
                int qte = int.Parse(Console.ReadLine());

                LigneCommande ligneCmd = new LigneCommande() { Produit = prod, Quantite = qte };
                lignesCommande.Add(ligneCmd);
                Console.WriteLine("Appuyer sur 'q' si vous souhaitez arrêter d'ajouter des produits, sinon, appuyez sur n'importe quelle touche");
                input = Console.ReadLine();
            } while (!"q".Equals(input));
            return lignesCommande;
        }

        private Produit AjouterProduit()
        {
            Console.WriteLine("Entrez l'ID d'un produit à ajouter à la commande : ");
            ICollection<Produit> produits = controleur.GetProduits();
            foreach (Produit produit in produits)
            {
                Console.WriteLine(produit.Id + " : " + produit.Designation);
            }
            string input = Console.ReadLine();
            return produits.Where(p => p.Id == int.Parse(input)).First();
        }

        private void AfficherClients()
        {
            foreach (Client client in controleur.GetClients())
            {
                Console.WriteLine(client.Id + " : " + client.Prenom + " " + client.Nom);
            }
        }

        private void AjouterNouveauClient()
        {
            Console.WriteLine("Ajout d'un nouveau client");
            Console.WriteLine("Veuillez entrer le nom du client");
            string nom = Console.ReadLine();
            Console.WriteLine("Veuillez entrer le prenom du client");
            string prenom = Console.ReadLine();
            Console.WriteLine("Veuillez entrer l'adresse mail du client");
            string mail = Console.ReadLine();
            controleur.CreerClient(nom, prenom, mail);
        }

        private void AjouterNouveauProduit()
        {
            Console.WriteLine("Ajout d'un nouveau produit");
            Console.WriteLine("Veuillez entrer la designation du produit");
            string designation = Console.ReadLine();
            Console.WriteLine("Veuillez entrer le prix du produit");
            int prix = int.Parse(Console.ReadLine());
            controleur.CreerProduit(designation, prix);
        }
    }
}
