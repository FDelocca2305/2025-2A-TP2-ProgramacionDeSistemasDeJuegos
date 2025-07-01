using UnityEngine;

public class SimpleCharacterFactory : ICharacterFactory
{
    public GameObject CreateCharacter(CharacterConfig config, Vector3 position, Quaternion rotation)
    {
        var result = Object.Instantiate(config.prefab, position, rotation);

        if (result.TryGetComponent(out Character character))
            character.Setup(config.characterModel);

        if (result.TryGetComponent(out PlayerController controller))
            controller.Setup(config.controllerModel);

        var animator = result.GetComponentInChildren<Animator>();
        if (!animator)
            animator = result.gameObject.AddComponent<Animator>();
        animator.runtimeAnimatorController = config.animatorController;

        return result.gameObject;
    }
}
