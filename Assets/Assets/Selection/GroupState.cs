using System.Collections;
using System.Collections.Generic;
using FUBAR;
using UnityEngine;
using UnityEngine.AI;

namespace FUBAR
{

    public class GroupState : SelectionState
    {
        private FUBAR.SelectionController selectionController;
        private GroupSelectionManager SelectionManager;

        public GroupState(FUBAR.SelectionController selectionController, GroupSelectionManager selectionManager)
        {
            this.selectionController = selectionController;
            SelectionManager = selectionManager;
        }

        public override void Attach()
        {
            throw new System.NotImplementedException();
        }

        public override void Attack()
        {
            throw new System.NotImplementedException();
        }

        public override void Dettach()
        {
            throw new System.NotImplementedException();
        }

        public override void Init()
        {
            // throw new System.NotImplementedException();
        }

        public override void Move(MoveOrder order)
        {
            List<Group> groups = SelectionManager.GetGroup();
            foreach (Group item in groups)
            {
                foreach (ClickObject click in item.GetObjects())
                {
                    NavMeshAgent agent = click.GetComponent<NavMeshAgent>();
                    if (agent)
                        agent.SetDestination(order.Destination);
                }
            }
        }

        public override void StateLost()
        {
            SelectionManager.ResetSelection();
        }
    }
}