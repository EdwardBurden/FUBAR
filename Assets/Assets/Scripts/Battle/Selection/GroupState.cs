using System.Collections;
using System.Collections.Generic;
using FUBAR;
using UnityEngine;
using UnityEngine.AI;

namespace FUBAR
{

    public class GroupState : SelectionState
    {
        private GroupSelectionManager SelectionManager;

        public GroupState(GroupSelectionManager selectionManager)
        {
            SelectionManager = selectionManager;
        }

        public override void Attach(AttachOrder attachOrder)
        {
            List<Group> groups = SelectionManager.GetGroup();
            foreach (Group item in groups)
            {
                foreach (ClickObject click in item.GetObjects())
                {
                    AttachComponent comp = click.GetComponent<AttachComponent>();
                    if (comp)
                        comp.Attach(attachOrder.Anchor.transform);
                }
            }
        }

        public override void Attack(AttackOrder order)
        {
            List<Group> groups = SelectionManager.GetGroup();
            foreach (Group item in groups)
            {
                foreach (ClickObject click in item.GetObjects())
                {
                    AttachComponent comp = click.GetComponent<AttachComponent>();
                    if (comp)
                        comp.Dettach();
                    NavMeshAgent agent = click.GetComponent<NavMeshAgent>();
                    if (agent)
                        agent.SetDestination(order.GetShootingDistance(agent.transform.position));
                }
            }
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
                item.Move(order);
            }
        }

        public override void StateLost()
        {
            SelectionManager.ResetSelection();
        }
    }
}