using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Scripts._Systems.Initializer
{
    [DefaultExecutionOrder(-100)] // Ensures this runs before other standard scripts
    public class Bootstrapper : MonoBehaviour
    {
        private static Bootstrapper instance;

        [Header("Configuration")]
        [SerializeField] private string firstGameplayScene = "MainLevel";

        private void Awake()
        {
            // Implement Singleton pattern to prevent duplicates if returning to the boot scene
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);

            InitializeCoreSystems();
        }

        private void Start()
        {
            // Load the initial game scene after systems are initialized
            LoadInitialScene();
        }

        private void InitializeCoreSystems()
        {
            Debug.Log("[Bootstrapper] Initializing core systems...");

            // TODO: Initialize your managers here
            // Example: SaveManager.Initialize();
            // Example: AudioManager.Initialize();
        
            Debug.Log("[Bootstrapper] Core systems initialized successfully.");
        }

        private void LoadInitialScene()
        {
            // If we started the game directly from a gameplay scene during development, 
            // you might want to skip auto-loading the main level.
            if (SceneManager.GetActiveScene().name == "BootScene")
            {
                SceneManager.LoadScene(firstGameplayScene);
            }
        }
    }
}
