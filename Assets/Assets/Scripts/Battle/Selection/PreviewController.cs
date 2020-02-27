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




        public void BeginPreview(List<Vector3> poss, PreviewOrder previewOrder, List<ClickObject> clickObjects)
        {
            Clean();
            StartPos = previewOrder.start;
            if (CanGenerate(previewOrder))
            {
                for (int i = 0; i < clickObjects.Count; i++)
                {
                    Previews.Add(GameObject.Instantiate(clickObjects[i].GetPreviewObject(), poss[i], Quaternion.identity, PreviewSpawn));
                }
            }
        }


        public void Preview(List<Vector3> poss, PreviewOrder previewOrder, List<ClickObject> clickObjects)
        {
            if (CanGenerate(previewOrder))
            {
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