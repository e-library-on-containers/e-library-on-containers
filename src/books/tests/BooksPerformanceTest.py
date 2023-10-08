import uuid
import jwt
from locust import HttpUser, task, between
 
 
class MyUser(HttpUser):
    wait_time = between(1, 5)
 
    def on_start(self):
        self.client.verify = False

    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)
        self.create_book_body = {
            "ISBN": "11111111134",
            "Authors": "autor",
            "Title": "tile",
            "Description": "opis",
            "coverImg": "./"
        }
        self.create_book_instance_body = {
            "ISBN": "1111111111111",
            "IsAvailable": True,
        }

    @task
    def create_and_delete_book(self):
        self.client.post("/books", json=self.create_book_body)
        self.client.delete("/books/11111111134")

    @task
    def create_book_instance(self):
        self.client.post("/bookinstances", json=self.create_book_instance_body)

    @task
    def get_books(self):
        response = self.client.get("/books")

    @task
    def get_book(self):
        response = self.client.get("/books/1111111111111")

    @task
    def get_book_instances(self):
        response = self.client.get("/bookinstances")

    @task
    def get_book_instance(self):
        response = self.client.get("/bookinstances/11")

    @task
    def get_book_instancs_of_book(self):
        response = self.client.get("/bookinstances?ISBN=\"1111111111111\"")
