    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCallback : MonoBehaviour
{
    public Hero hero;
    public void DidChain(int chain)
    {
        hero.DidChain(chain);
    }
}
