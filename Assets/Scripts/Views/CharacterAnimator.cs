using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private string speedParameter = "Speed";
    [SerializeField] private string isJumpingParameter = "IsJumping";
    [SerializeField] private string isFallingParameter = "IsFalling";
    
    private static readonly List<CharacterAnimator> _instances = new();
    private Coroutine _overrideCoroutine;
    private bool _isOverriding = false;
    
    public static IReadOnlyList<CharacterAnimator> Instances => _instances;
    
    private void Reset()
    {
        character = GetComponentInParent<Character>();
        animator = GetComponentInParent<Animator>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
    }

    private void Awake()
    {
        if (!character)
            character = GetComponentInParent<Character>();
        if (!animator)
            animator = GetComponentInParent<Animator>();
        if (!spriteRenderer)
            spriteRenderer = GetComponentInParent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        if (!_instances.Contains(this))
            _instances.Add(this);
        
        if (!character || !animator || !spriteRenderer)
        {
            Debug.LogError($"{name} <color=grey>({GetType().Name})</color>: At least one reference is null!");
            enabled = false;
        }
    }

    private void Update()
    {
        if (_isOverriding)
            return;
        
        var speed = character.Velocity;
        animator.SetFloat(speedParameter, Mathf.Abs(speed.x));
        animator.SetBool(isJumpingParameter, character.Velocity.y > 0);
        animator.SetBool(isFallingParameter, character.Velocity.y < 0);
        spriteRenderer.flipX = speed.x < 0;
    }
    
    private void OnDisable()
    {
        _instances.Remove(this);
    }
    
    public void ForceAnimation(AnimationCommandConfig animConfig, float duration = 1f)
    {
        if (_overrideCoroutine != null)
            StopCoroutine(_overrideCoroutine);
        _overrideCoroutine = StartCoroutine(ForceAnimationRoutine(animConfig, duration));
    }

    private IEnumerator ForceAnimationRoutine(AnimationCommandConfig animConfig, float duration)
    {
        _isOverriding = true;
        foreach (var param in animConfig.parameters)
        {
            switch (param.type)
            {
                case AnimatorControllerParameterType.Bool:
                    animator.SetBool(param.name, param.boolValue);
                    break;
                case AnimatorControllerParameterType.Float:
                    animator.SetFloat(param.name, param.floatValue);
                    break;
                case AnimatorControllerParameterType.Trigger:
                    animator.SetTrigger(param.name);
                    break;
            }
        }
        animator.Play(animConfig.animatorStateName);

        yield return new WaitForSeconds(duration);

        _isOverriding = false;
        _overrideCoroutine = null;
    }
}
