using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleWarrior
{
    public class Camera : Entity
    {
        HashSet<Entity> inView = new HashSet<Entity>();
        private IDriver driver;

        public Camera(IDriver driver)
        {
            this.driver = driver;            
            shape = new AreaShape(-50,-50,100,100);

        }


        protected internal override void EnterCollision(Entity other)
        {
            inView.Add(other);
        }
        protected internal override void ExitCollision(Entity other)
        {
            inView.Remove(other);
        }

        internal void Render(double elapsed)
        {

            var transformedRequests = inView
                .OfType<IVisible>()
                .SelectMany(x => x.Render(elapsed))
                .OrderBy(x => x.Z);
                

            foreach(var request in transformedRequests)
            {
                request.TranslateTransform(-X, -Y, 0);
                driver.DrawSprite(request);
            }
        }
    }
}
