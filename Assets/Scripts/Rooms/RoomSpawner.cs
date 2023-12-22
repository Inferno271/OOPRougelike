using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс RoomSpawner, отвечающий за создание комнат в игре.
public class RoomSpawner : MonoBehaviour
{
    // Публичная переменная для указания направления открытия комнаты.
    public int openingDirection;

    // Приватная переменная для хранения ссылки на шаблоны комнат.
    private RoomTemplates templates;

    // Приватная переменная для хранения случайного значения.
    private int rand;

    // Приватная переменная, отслеживающая, была ли уже создана комната.
    private bool spawned = false;

    // Публичная переменная для установки времени ожидания перед уничтожением объекта.
    public float waitTime = 2f;

    // Метод Start вызывается при инициализации скрипта.
    private void Start()
    {
        // Уничтожает этот объект через заданное время (waitTime).
        Destroy(gameObject, waitTime);

        // Находит объект с тегом "Rooms" и получает компонент RoomTemplates.
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();

        // Вызывает метод Spawn через 0.2 секунды.
        Invoke("Spawn", 0.2f);
    }

    // Метод для создания комнаты.
    private void Spawn()
    {
        // Проверка, не была ли комната уже создана.
        if (!spawned)
        {
            // Выбирает тип комнаты на основе направления открытия и создает ее.
            if (openingDirection == 1)
            {
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            }
            else if (openingDirection == 2)
            {
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
            }
            else if (openingDirection == 3)
            {
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            }
            else if (openingDirection == 4)
            {
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }

            // После создания комнаты устанавливает флаг spawned в true.
            spawned = true;
        }
    }

    // Метод вызывается при входе в триггер (коллайдер).
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверка на столкновение с другими точками создания комнат.
        if (other.CompareTag("SpawnPoint"))
        {
            // Проверка, что ни одна из точек не создала комнату.
            if (!other.GetComponent<RoomSpawner>().spawned && !spawned)
            {
                // Создание закрытой комнаты и уничтожение этого объекта.
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            // Установка флага spawned в true.
            spawned = true;
        }
    }
}
