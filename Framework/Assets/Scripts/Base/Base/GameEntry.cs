using System.Collections;
using Log;
using UnityEngine;

namespace Base.Base
{
    public partial class GameEntry : MonoBehaviour
    {
        private static readonly CacheLinkedList<GameComponent> GameComponents = new CacheLinkedList<GameComponent>();

        private IEnumerator Start()
        {
            var current = GameComponents.First;
            while (current != null)
            {
                current.Value.Init();
                current = current.Next;
                yield return null;
            }

            yield return null;
        }
        public static void RegisterComponent(GameComponent gameComponent)
        {
            if (gameComponent == null)
            {
                D.Error("gameComponent '{0}' is invalid");
            }

            var type = gameComponent.GetType();
            var current = GameComponents.First;
            while (current != null)
            {
                if (current.Value.GetType() == type)
                {
                    D.Error("Game Framework component type '{0}' is already exist.", type.FullName);
                    return;
                }

                if (gameComponent.Priority > current.Value.Priority)
                {
                    break;
                }

                current = current.Next;
            }

            if (current != null)
            {
                GameComponents.AddBefore(current, gameComponent);
            }
            else
            {
                GameComponents.AddLast(gameComponent);
            }
        }
    }
}