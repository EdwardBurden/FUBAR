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

        [SerializeField]
        private int DragThreshold = 5; //temp

        [SerializeField]
        private int Space = 5; //temp

        private int Columns = 4;
        private List<GameObject> Previews;
        [SerializeField]
        private Transform PreviewSpawn;

        private void Awake()
        {
            Instance = this;
        }


        public void BeginPreview(PreviewOrder previewOrder, List<ClickObject> clickObjects)
        {
            Clean();
            StartPos = previewOrder.start;
            if (CanGenerate(previewOrder))
            {
                List<Vector3> poss = Formations.OrganiseSquareFormationFromCorner(previewOrder.start, previewOrder.end, clickObjects, Columns, Space);
                for (int i = 0; i < clickObjects.Count; i++)
                {
                    Previews.Add(GameObject.Instantiate(clickObjects[i].GetPreviewObject(), poss[i], Quaternion.identity, PreviewSpawn));
                }
            }
        }

        public void Preview(PreviewOrder previewOrder, List<ClickObject> clickObjects)
        {
            if (CanGenerate(previewOrder))
            {
                float endDiffference = Vector3.Distance(previewOrder.end, StartPos);
                Columns = ((int)endDiffference / Space) + 1;//5 = space
                List<Vector3> poss = Formations.OrganiseSquareFormationFromCorner(StartPos, previewOrder.end, clickObjects, Columns, Space);
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
            if (PreviousPos != null)
            {
                float endDiffference = Vector3.Distance(previewOrder.end, PreviousPos.end);
                if (endDiffference > DragThreshold)
                {
                    return true;
                }
                else return false;
            }
            else return true;

        }
    }
}