using UnityEngine;
using System.Collections.Generic;

namespace Steer2D
{
    public class FollowPath : SteeringBehaviour
    {
        public Vector2[] Path;
        public float SlowRadius = 1;
        public float StopRadius = 0.2f;
        public float NextCoordRadius = 0.2f;
        public bool Loop = false;
		public Transform Leader;
		public Vector2 oldPosition;
		public int SeedNumber;
		private bool facingRight = true;


        public bool DrawGizmos = false;

        public bool Finished
        {
            get
            {
                return currentPoint >= Path.Length;
            }
        }
		int LeCounter = 0;
        int currentPoint = 0;
		int LeaderPosition = 0;

        public void SetNewPath(Vector2[] path)
        {
            Path = path;
            currentPoint = 0;
			LeaderPosition = 0;
        }

		public void Update()
		{
			Vector2 Pos = (Vector2)Leader.position;
			if(Vector2.Distance(Pos, Path[currentPoint]) < SeedNumber)
			{
				if(LeaderPosition < 30)
				{
					switch(LeaderPosition)
					{
					case 1:
						currentPoint = Path.Length-2;
						break;
					case 0:
						currentPoint = Path.Length-3;
						break;
					}

				}
				else
				{
					currentPoint = LeaderPosition - 2;
				}


			}

			
			if (oldPosition != Pos)
			{
				if(Pos.x > oldPosition.x)
				{
					facingRight = true;
				} 
				else if(Pos.x < oldPosition.x)
				{
					facingRight = false;
				}
				if(facingRight)
				{
					Pos.x = Pos.x - SeedNumber;
				} 
				else 
				{
					Pos.x = Pos.x + SeedNumber;
				}

	
				Path[LeaderPosition] = Pos;
				if(LeaderPosition < Path.Length - 1)
				{
					LeaderPosition++;
				}
				else
				{
					LeaderPosition = 0;
				}

				oldPosition = (Vector2)Leader.position;

			}
		}

        public override Vector2 GetVelocity()
        {
            Vector2 velocity;

            if (currentPoint >= Path.Length && currentPoint != LeaderPosition)
                return Vector2.zero;
            else if (!Loop && currentPoint == Path.Length - 1)
                velocity = arrive(Path[currentPoint]);
            else
                velocity = seek(Path[currentPoint]);

            float distance = Vector3.Distance(transform.position, Path[currentPoint]);
            if ((currentPoint == Path.Length - 1 && distance < StopRadius) || distance < NextCoordRadius)
            {
                currentPoint++;
                if (Loop && currentPoint == Path.Length)
                    currentPoint = 0;
            }

            return velocity;
        }

        Vector2 seek(Vector2 targetPoint)
        {
            return ((targetPoint - (Vector2)transform.position).normalized * agent.MaxVelocity) - agent.CurrentVelocity;   
        }

        Vector2 arrive(Vector2 targetPoint)
        {
            float distance = Vector3.Distance(transform.position, (Vector3)targetPoint);
            Vector2 desiredVelocity = (targetPoint - (Vector2)transform.position).normalized;

            if (distance < StopRadius)
                desiredVelocity = Vector3.zero;
            else if (distance < SlowRadius)
                desiredVelocity = desiredVelocity * agent.MaxVelocity * ((distance - StopRadius) / (SlowRadius - StopRadius));
            else
                desiredVelocity = desiredVelocity * agent.MaxVelocity;

            return desiredVelocity - agent.CurrentVelocity;
        }

        void OnDrawGizmos()
        {
            if (DrawGizmos)
            {
                if (currentPoint < Path.Length)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(Path[currentPoint], .05f);

                    if (currentPoint == Path.Length - 1)
                    {
                        Gizmos.color = Color.blue;
                        Gizmos.DrawWireSphere(Path[currentPoint], SlowRadius);

                        Gizmos.color = Color.red;
                        Gizmos.DrawWireSphere(Path[currentPoint], StopRadius);
                    }
                    else
                    {
                        Gizmos.color = Color.blue;
                        Gizmos.DrawWireSphere(Path[currentPoint], NextCoordRadius);
                    }
                }

                Gizmos.color = Color.magenta;
                for (int i = 0; i < Path.Length - 1; ++i)
                {
                    Gizmos.DrawLine(Path[i], Path[i + 1]);
                }
            }
        }
    }
}