using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TestingCombinatoriale
{
    public static class Minimizzazione
    {
        public static List<int> Minimizza(int[,] matrice)
        {
            var numRighe = matrice.GetLength(0);
            var numColonne = matrice.GetLength(1);
            var soluzione = new List<int>();

            var indiciRighe = new List<int>(Enumerable.Range(0, numRighe).ToArray());
            var indiciColonne = new List<int>(Enumerable.Range(0, numColonne).ToArray());

            var stop = false;

            for (var i = 0; i < numRighe; i++)
            for (var j = i; j < numRighe; j++)
            {
                if (i == j) continue;
                var equal = true;
                for (var k = 0; k < numColonne; k++)
                    if (matrice[i, k] != matrice[j, k])
                        equal = false;

                if (equal) indiciRighe.Remove(j);
            }

            if (indiciRighe.Count == 1)
            {
                soluzione.Add(indiciRighe.First());
                stop = true;
            }

            

            while (!stop)
            {
                if (indiciRighe.Count == 0 || indiciColonne.Count == 0)
                {
                    stop = true;
                    continue;
                }

                var essentialFound = false;
                var essentialRowId = 0;

                //criterio di essenzialità

                foreach (var t in indiciColonne)
                {
                    var onescount = 0;
                    var temp = new List<int>();
                    foreach (var t1 in indiciRighe.Where(t1 => matrice[t1, t] == 1))
                    {
                        onescount++;
                        temp.Add(t1);
                    }

                    if (onescount != 1) continue;
                    essentialFound = true; //riga essenziale trovata
                    essentialRowId = temp[0];
                    break;
                }

                var tempCol = indiciColonne.ToArray().ToList();

                if (essentialFound)
                {
                    foreach (var t in indiciColonne.Where(t => matrice[essentialRowId, t] == 1)) tempCol.Remove(t);

                    indiciColonne = tempCol.ToArray().ToList();

                    indiciRighe.Remove(essentialRowId);
                    soluzione.Add(essentialRowId);
                    // essentialFound = false;
                }


                var rigaeliminata = false;

                var lstRighe = (from indiceRiga in indiciRighe
                    let id = indiceRiga
                    let colonneCoperte =
                        indiciColonne.Where(indiceColonna => matrice[indiceRiga, indiceColonna] == 1).ToList()
                    select new RigaMatrice(indiceRiga, colonneCoperte)).ToList();

                foreach (var rigaRoot in lstRighe)
                {
                    var lstRigheTemp = lstRighe.ToArray().ToList();
                    lstRigheTemp.Remove(lstRighe.Single(r => r.Id == rigaRoot.Id));
                    foreach (var rigaChild in lstRigheTemp.Where(rigaChild =>
                        ContainsAllItems(rigaRoot.IDcolonneCoperte, rigaChild.IDcolonneCoperte) &&
                        rigaRoot.IDcolonneCoperte.Count > rigaChild.IDcolonneCoperte.Count))
                    {
                        indiciRighe.Remove(rigaChild.Id);
                        rigaeliminata = true;
                    }
                }


                var colonnaeliminata = false;

                var lstColonne = (from indiceColonna in indiciColonne
                    let id = indiceColonna
                    let righeCoperte = indiciRighe.Where(indiceRiga => matrice[indiceRiga, indiceColonna] == 1).ToList()
                    select new ColonnaMatrice(indiceColonna, righeCoperte)).ToList();

                foreach (var colonnaRoot in lstColonne)
                {
                    var lstColonneTemp = lstColonne.ToArray().ToList();
                    lstColonneTemp.Remove(lstColonne.Single(r => r.Id == colonnaRoot.Id));
                    foreach (var colonnaChild in lstColonneTemp.Where(colonnaChild =>
                        ContainsAllItems(colonnaRoot.IDrigheCoperte, colonnaChild.IDrigheCoperte) &&
                        colonnaRoot.IDrigheCoperte.Count > colonnaChild.IDrigheCoperte.Count))
                    {
                        indiciColonne.Remove(colonnaRoot.Id);
                        colonnaeliminata = true;
                    }
                }

                if (essentialFound || rigaeliminata || colonnaeliminata) continue;
                {
                    //int randIndex = rand.Next(0, indiciRighe.Count);
                    //int rowID = indiciRighe[randIndex];
                    //indiciRighe.Remove(rowID);
                    //soluzione.Add(rowID);

                    var obj = lstRighe.Where(r =>
                        r.IDcolonneCoperte.Count == lstRighe.Max(m => m.IDcolonneCoperte.Count));
                    var riga = obj.ElementAt(0);

                    var rowId = riga.Id;

                    foreach (var idColonna in riga.IDcolonneCoperte) indiciColonne.Remove(idColonna);

                    indiciRighe.Remove(rowId);
                    soluzione.Add(rowId);
                }
            }


            return soluzione;
        }

        public static bool ContainsAllItems(List<int> a, List<int> b)
        {
            return !b.Except(a).Any();
        }
    }
}