using SentienceLab.OSC;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[DisallowMultipleComponent]

public class TrackedHead : MonoBehaviour
{
	public void Start()
	{
		m_face = GetComponent<ARFace>();
		if (m_face != null)
		{
			m_face.updated += OnFaceUpdated;
		}
		m_osc = FindFirstObjectByType<OSC_Manager>();
		m_pose = new OSC_6DofPoseVariable("/fpaosc/6dof", false);
		m_pose.SetManager(m_osc);
	}


	public void OnFaceUpdated(ARFaceUpdatedEventArgs _args)
	{
		if (_args.face.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
		{
			m_pose.Position = _args.face.pose.position * 100.0f; // send in cm
			m_pose.SendUpdate();
		}
	}

	protected ARFace               m_face;
	protected OSC_Manager          m_osc;
	protected OSC_6DofPoseVariable m_pose;
}
