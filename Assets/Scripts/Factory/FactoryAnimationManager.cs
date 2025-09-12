using UnityEngine;

public class FactoryAnimationManager : MonoBehaviour
{
    public Animator PokemonInfoAnimator;
    public Animator[] PokeballAnimator = new Animator[6];

    public void SetPokemonInfoAnimator(bool boolean)
    {
        PokemonInfoAnimator.SetBool("isActive", boolean);
    }

    public void SetPokeballAnimator(int index, string name, bool boolean)
    {
        PokeballAnimator[index].SetBool(name, boolean);
    }

    public void EnablePokemonInfoAnimator()
    {
        PokemonInfoAnimator.enabled = true;
    }

    public bool IsAnimationPlaying(Animator animator, string name, float time)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(name) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= time)
            return true;
        else
            return false;
    }
}