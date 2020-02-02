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


        private int MinDistance;
        private int MaxDistance;

        private bool Init;

        public PreviewManager(Transform previewSpawn)
        {
            PreviewSpawn = previewSpawn;
            Previews = new List<GameObject>();
            Init = false;
        }

        public void GeneratePreview(PreviewOrder previewOrder, List<ClickObject> clickObjects)
        {
            if (CanGenerate(previewOrder))
            {

                if (StartOrder == null)
                {
                    StartOrder = previewOrder;

                }
                int columns = 4;
                if (LastOrder != null)
                {
                    float endDiffference = Vector3.Distance(previewOrder.end, StartOrder.end);
                    columns = ((int)endDiffference / 5) + 1;//5 = space
                }
                float angle = Vector3.Angle(Vector3.forward, (previewOrder.start - previewOrder.end).normalized);
                List<Vector3> poss = Formations.OrganiseSquareFormationFromCorner(previewOrder.start, previewOrder.end, clickObjects, columns, 5); //movenumbers to constant script

                for (int i = 0; i < clickObjects.Count; i++)
                {
                    if (!Init)
                    {
                        Previews.Add(GameObject.Instantiate(clickObjects[i].GetPreviewObject(), poss[i], Quaternion.identity, PreviewSpawn));
     
                    }
                    else
                        Previews[i].transform.position = poss[i];
                }
                Init = true;
                LastOrder = previewOrder;


            }
        }

        public void Clean()
        {
            StartOrder = null;
            LastOrder = null;
            foreach (GameObject item in Previews)
            {
                GameObject.Destroy(item.gameObject);
            }
        }


        private bool CanGenerate(PreviewOrder previewOrder)
        {

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