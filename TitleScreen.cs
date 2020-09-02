using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gold {
    public class TitleScreen : MonoBehaviour {
        [SerializeField] Dropdown settingsResolutionDropdown;
        [SerializeField] Dropdown qualityDropdown;
        [SerializeField] Toggle settingsFullscreenToggle;
        [SerializeField] GameObject settings;
        [SerializeField] GameObject titleScreen;

        List<string> resolutionOptions = new List<string> ();

        void Awake () {
            ShowTitleScreen ();
        }

        void RefreshScreens () {
            settingsResolutionDropdown.ClearOptions ();
            resolutionOptions.Clear ();

            int currentSetting = 0;
            for (int i = 0; i < Screen.resolutions.Length; i++) {
                resolutionOptions.Add (Screen.resolutions[i].ToString ());

                Debug.Log (string.Format ("{0} {1} {2}", Screen.width, Screen.height, Application.targetFrameRate));
                if (Screen.width == Screen.resolutions[i].width &&
                    Screen.height == Screen.resolutions[i].height) {
                    Debug.Log (i.ToString () + " " + Screen.resolutions[i].ToString ());
                    currentSetting = i;
                }
            }
            settingsResolutionDropdown.AddOptions (resolutionOptions);

            settingsResolutionDropdown.value = currentSetting;
            settingsFullscreenToggle.isOn = Screen.fullScreen;
            qualityDropdown.value = QualitySettings.GetQualityLevel ();
        }

        public void SettingsApply () {
            UnityEngine.Resolution res = UnityEngine.Screen.resolutions[settingsResolutionDropdown.value];
            QualitySettings.SetQualityLevel (qualityDropdown.value);
            UnityEngine.Screen.SetResolution (res.width, res.height, settingsFullscreenToggle.isOn ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed, res.refreshRate);
        }

        public void ShowTitleScreen () {
            settings.SetActive (false);
            titleScreen.SetActive (true);
        }

        public void ShowSettings () {
            settings.SetActive (true);
            titleScreen.SetActive (false);
            RefreshScreens ();
        }

        public void StartGame () {
            UnityEngine.SceneManagement.SceneManager.LoadScene (1);
        }

        public void ExitGame () {
            Application.Quit ();
        }
    }
}