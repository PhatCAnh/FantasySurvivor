using System.Linq;
using UnityEngine;
namespace FantasySurvivor
{
	public class VongQuayMayMan : SkillActive
	{
	
    Transform characterTransform;
    
    public override void Init(float damage, Monster target, int level) {
        base.Init(damage, target, level);
        
        characterTransform = gameController.character.transform;
        transform.position = characterTransform.position; 
    }

    //    protected override void TakeDamage(Monster mons)
    //    {

    //        // Tính vector từ nhân vật tới quái
    //        Vector3 dir = (mons.transform.position - characterTransform.position).normalized;
        
    //    // Tạo hiệu ứng đánh theo vector đó 
    //    GameObject hitEffectInstance = Instantiate(hitEffect, characterTransform.position + dir, Quaternion.identity);
        
    //    // Xoay hiệu ứng về phía quái 
    //    hitEffectInstance.transform.forward = dir;

    //    mons.TakeDamage(damage, isCritical);
    //}
    
}
}