namespace rose.row.actor.ai
{
    public class AiCheckGround : AiBehaviour
    {
        private void Update()
        {
            if (ai.controller.IsSprinting() && !ai.controller.OnGround())
            {
                ai.forceFallOver();
            }
        }
    }
}
