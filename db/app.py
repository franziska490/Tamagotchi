from flask import Flask, jsonify, request
import mysql.connector 

app = Flask(__name__)

db = mysql.connector.connect(
    host="localhost",
    user="root",
    password="3172",
    database="tamagotchidb"
)

@app.route("/pets",methods=["GET"])
def get_pets():
    cursor = db.cursor()
    cursor.execute("SELECT * FROM pets")
    rows = cursor.fetchall()

    pets=[]
    for row in rows:
        pets.append({
             "petid":row[0], "name":row[1], "hunger":row[2],"energy":row[3],"mood":row[4],"ownerid":row[5]})

    return jsonify(pets),200


if __name__=='__main__':
    app.run()
