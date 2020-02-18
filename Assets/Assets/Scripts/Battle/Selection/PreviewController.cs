using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FUBAR
{
    public class PreviewController : MonoBehaviour
    {
        public static PreviewController Instance;

        private Vector3 StartPos;
        private PreviewOrder PreviousPos;

        private List<GameObject> Previews;
        [SerializeField]
        private Transform PreviewSpawn;

        private void Awake()
        {
            Instance = this;
        }




        public void BeginPreview(PreviewOrder previewOrder, List<ClickObject> clickObjects, int columns = Constants.K_DefaultColumns, int space = Constants.K_DefaultSpace)
        {
            Clean();
            StartPos = previewOrder.start;
            if (CanGenerate(previewOrder))
            {
                List<Vector3> poss;
                if (!previewOrder.Drag)
                {
                    poss = Formations.OrganiseSquareFormation(previewOrder.start, clickObjects, columns, space);
                }
                else
                {
                    poss = Formations.OrganiseSquareFormationFromCorner(previewOrder.start, previewOrder.end, clickObjects, columns, space);
                }
                for (int i = 0; i < clickObjects.Count; i++)
                {
                    Previews.Add(GameObject.Instantiate(clickObjects[i].GetPreviewObject(), poss[i], Quaternion.identity, PreviewSpawn));
                }
            }
        }


        public void Preview(PreviewOrder previewOrder, List<ClickObject> clickObjects, int space = Constants.K_DefaultSpace)
        {
            if (CanGenerate(previewOrder))
            {
                int columns = Formations.GetColumnsFromDistance(StartPos, previewOrder.end);
                List<Vector3> poss = Formations.OrganiseSquareFormationFromCorner(StartPos, previewOrder.end, clickObjects, columns, space);
                for (int i = 0; i < clickObjects.Count; i++)
                {
                    Previews[i].transform.position = poss[i];
                }
                PreviousPos = previewOrder;
            }
        }

        public void EndPreview()
        {
            Clean();
        }

        private void Clean()
        {
            StartPos = Vector3.zero;
            PreviousPos = null;
            if (Previews == null)
                Previews = new List<GameObject>();
            foreach (GameObject item in Previews)
            {
                GameObject.Destroy(item.gameObject);
            }
            Previews.Clear();
        }

        private bool CanGenerate(PreviewOrder previewOrder)
        {

            return true;


        }
    }
}