using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Creatures
{
    public class CreatureController : EntityConrtoller
    {
        public Creature CreatureParent
        {
            get => (Creature)EntityParent;
            set => EntityParent = value;
        }

        public virtual float HorizontalMoove => 0;
        public virtual float VerticalMoove => 0;

        public virtual bool IsJumped => false;
    }
}
