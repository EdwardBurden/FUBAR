using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FUBAR
{
    public class PreviewManager
    {
        private PreviewOrder StartOrder;
        private PreviewOrder LastOrder;
        private int ChangeAmount = 5;
        private List<GameObject> Previews;
        private Transform PreviewSpawn;

        public PreviewManager(Transform previewSpawn)
        {
            PreviewSpawn = previewSpawn;
            Previews = new List<GameObject>();
        }

        public void GeneratePreview(PreviewOrder previewOrder, List<ClickObject> clickObjects)
        {
            if (CanGenerate(previewOrder))
            {
                foreach (GameObject item in Previews)
                {
                    GameObject.Destroy(item.gameObject);
                }

                int columns = 4;
                if (LastOrder != null)
                {
                    float endDiffference = Vector3.Distance(previewOrder.end, StartOrder.end);
                    //   if (endDiffference > ChangeAmount)
                    //  {
                    columns = ((int)endDiffference / 5) + 1;//5 = space

                    Debug.Log(endDiffference + ":" + columns);
                    //     }

                }
                List<Vector3> poss = Formations.OrganiseSquareFormation(previewOrder.start, clickObjects, columns, 5); //movenumbers to constant script

                for (int i = 0; i < clickObjects.Count; i++)
                {
                    Previews.Add(GameObject.Instantiate(clickObjects[i].GetPreviewObject(), poss[i], Quaternion.identity, PreviewSpawn));
                }
                LastOrder = previewOrder;
            }
        }

        public void Clean()
        {
            StartOrder = null;
            LastOrder = null;

        }


        private bool CanGenerate(PreviewOrder previewOrder)
        {
            if (StartOrder == null)
            {
                StartOrder = previewOrder;
            }
            if (LastOrder != null)
            {
                float endDiffference = Vector3.Distance(previewOrder.end, LastOrder.end);
                if (endDiffference > ChangeAmount)
                {
                    return true;
                }
                else return false;
            }
            else return true;

        }
    }
}