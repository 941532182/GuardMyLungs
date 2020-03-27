using System;
using UnityEngine;
using Manager;

namespace Controller
{
    [DisallowMultipleComponent]
    public class ProjectileController : MonoBehaviour
    {

        public Projectile CreateProjectile(long projectileId, Vector3 projectilePos, params object[] parameters)
        {
            var proj = Instantiate(Resources.Load<Transform>($"Prefabs/Unit/{ProjectileManager.GetPrefab(projectileId)}"));
            proj.position = projectilePos;
            proj.SetParent(transform);

            var proj_comp = proj.gameObject.AddComponent<Projectile>();
            proj_comp.ProjectileId = projectileId;

            ProjectileMoveController proj_move_ctrl = null;
            switch (ProjectileManager.GetBehaviour(projectileId))
            {
                case 0:
                    proj_move_ctrl = proj.gameObject.AddComponent<MissileMoveController>();
                    break;
                case 1:
                    proj_move_ctrl = proj.gameObject.AddComponent<LaserMoveController>();
                    break;
                case 2:
                    proj_move_ctrl = proj.gameObject.AddComponent<FakerMoveController>();
                    break;
            }
            proj_move_ctrl.Target = parameters[0] as Transform;
            proj_move_ctrl.ProjectileId = projectileId;

            var proj_exp_ctrl = proj.gameObject.AddComponent<ProjectileExplodeController>();
            proj_exp_ctrl.ProjectileId = projectileId;
            proj_exp_ctrl.TargetLayer = LayerMask.NameToLayer("Virus");
            proj_exp_ctrl.Damage = (int) parameters[1];
            proj_exp_ctrl.SpeedMultiplier = (float) parameters[2];
            proj_exp_ctrl.BuffTime = (float) parameters[3];
            proj_exp_ctrl.IsFire = (bool) parameters[4];

            return proj_comp;
        }

    }
}
