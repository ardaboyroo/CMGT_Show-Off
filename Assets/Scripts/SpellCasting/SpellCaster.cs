using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellCaster : MonoBehaviour
{
				[SerializeField] private InputActionReference triggerActionReference;
				[SerializeField] private Transform origin;
				[SerializeField] private Transform target;


				[Space(15)]
				[Header("Gun Spell")]
				[SerializeField] private GameObject bulletPrefab;
				[SerializeField] private float speed = 1f;
				[SerializeField] private float lifetime = 5f;


				[Space(10)]
				[Header("Fire Spell")]
				[SerializeField] private GameObject flamethrowerPrefab;
				[SerializeField] private GameObject torchPrefab;


				[Space(10)]
				[Header("Lightning Spell")]
				[SerializeField] private GameObject lightningStrikePrefab;
				[SerializeField] private GameObject sparksPrefab;


				[Space(10)]
				[Header("Earth Spell")]

				////////////////////////////////////
				// Private members
				////////////////////////////////////
				private SpellTypes currentSpell = SpellTypes.Gun;
				private float triggerValue;


				// Gun
				private bool readyToShoot = true;


				// Fire
				private GameObject flamethrowerPrefabInstance;
				private GameObject torchPrefabInstance;

				private ParticleSystem flamethrowerEmitter;
				private ParticleSystem torchEmitter;


				// Lightning
				private GameObject lightningStrikePrefabInstance;
				private GameObject sparksPrefabInstance;

				private ParticleSystem lightningStrikeEmitter;
				private ParticleSystem sparksEmitter;

				private void OnEnable()
				{
								CastDetector.OnCast += OnCastEventHandler;
				}

				private void OnDisable()
				{
								CastDetector.OnCast -= OnCastEventHandler;
				}

				#region EVENT_HANDLERS
				// Update is called once per frame
				void Update()
				{
								triggerValue = triggerActionReference.action.ReadValue<float>();

								switch (currentSpell)
								{
												case SpellTypes.Gun:
																OnUpdateGun();
																break;

												case SpellTypes.Fire:
																OnUpdateFire();
																break;

												case SpellTypes.Lightning:
																OnUpdateLightning();
																break;

												case SpellTypes.Earth:
																OnUpdateEarth();
																break;
								}
				}


				// Check for new spell
				void OnCastEventHandler(SpellTypes type)
				{
								//if (currentSpell != type) EndAllSpells();
								EndAllSpells();

								switch (type)
								{
												case SpellTypes.Gun:
																currentSpell = SpellTypes.Gun;
																OnCastGun();
																break;

												case SpellTypes.Fire:
																currentSpell = SpellTypes.Fire;
																OnCastFire();
																break;

												case SpellTypes.Lightning:
																currentSpell = SpellTypes.Lightning;
																OnCastLightning();
																break;

												case SpellTypes.Earth:
																currentSpell = SpellTypes.Earth;
																OnCastEarth();
																break;
								}
				}

				void EndAllSpells()
				{
								OnEndGun();
								OnEndFire();
								OnEndLightning();
								OnEndEarth();
				}
				#endregion

				//////////////////////
				// GUN SPELL
				//////////////////////
				void OnCastGun()
				{
								
				}

				void OnUpdateGun()
				{
								if (triggerValue > 0.75f && readyToShoot)
								{
												readyToShoot = false;

												GameObject bullet = Instantiate(bulletPrefab);
												bullet.transform.position = origin.position;
												bullet.transform.rotation = Quaternion.LookRotation(target.position - origin.position);

												bullet.GetComponent<MagicMissile>().SetLifeTime(lifetime);

												Rigidbody rb = bullet.GetComponent<Rigidbody>();
												rb.drag = 0;
												Vector3 direction = target.position - origin.position;
												rb.AddForce(direction * speed, ForceMode.Impulse);
								}
								else if (triggerValue < 0.75f)
								{
												readyToShoot = true;
								}
				}

				void OnEndGun()
				{

				}

				//////////////////////
				// FIRE SPELL
				//////////////////////
				void OnCastFire()
				{
								flamethrowerPrefabInstance = Instantiate(flamethrowerPrefab, transform.position, Quaternion.identity);
								flamethrowerEmitter = flamethrowerPrefabInstance.GetComponent<ParticleSystem>();

								torchPrefabInstance = Instantiate(torchPrefab, transform.position, Quaternion.identity);
								torchEmitter = torchPrefabInstance.GetComponent<ParticleSystem>();

								torchEmitter.Play();
				}

				void OnUpdateFire()
				{
								if (triggerValue > 0.75f)
								{
												if (torchEmitter.isPlaying)
																torchEmitter.Stop();

												if (!flamethrowerEmitter.isPlaying)
																flamethrowerEmitter.Play();

												// Move and rotate the particle
												flamethrowerPrefabInstance.transform.position = origin.position;
												flamethrowerPrefabInstance.transform.rotation = Quaternion.LookRotation(target.position - origin.position);
								}
								else
								{
												if (flamethrowerEmitter.isPlaying)
																flamethrowerEmitter.Stop();

												if (!torchEmitter.isPlaying)
																torchEmitter.Play();

												torchPrefabInstance.transform.position = target.position;
								}
				}

				void OnEndFire()
				{
								Destroy(flamethrowerPrefabInstance);
								Destroy(torchPrefabInstance);
				}

				//////////////////////
				// LIGHTNING SPELL
				//////////////////////
				void OnCastLightning()
				{
								lightningStrikePrefabInstance = Instantiate(lightningStrikePrefab);
								lightningStrikeEmitter = lightningStrikePrefabInstance.GetComponent<ParticleSystem>();

								sparksPrefabInstance = Instantiate(sparksPrefab, origin.position, Quaternion.identity);
								sparksEmitter = sparksPrefabInstance.GetComponent<ParticleSystem>();

								sparksEmitter.Play();
				}

				void OnUpdateLightning()
				{
								sparksPrefabInstance.transform.position = origin.position;

								if (triggerValue > 0.75f)
								{
												if (!lightningStrikeEmitter.isPlaying)
																lightningStrikeEmitter.Play();

												lightningStrikePrefabInstance.transform.position = origin.position;
												lightningStrikePrefabInstance.transform.rotation = Quaternion.LookRotation(Vector3.up);
								}
								else
								{
												if (lightningStrikeEmitter.isPlaying)
																lightningStrikeEmitter.Stop();
								}

								lightningStrikePrefabInstance.transform.position = origin.position;
				}

				void OnEndLightning()
				{
								Destroy(lightningStrikePrefabInstance);
								Destroy(sparksPrefabInstance);
				}

				//////////////////////
				// EARTH SPELL
				//////////////////////
				void OnCastEarth()
				{
								// something
				}

				void OnUpdateEarth()
				{

				}

				void OnEndEarth()
				{

				}
}
