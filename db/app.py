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


@app.route("/pets/<id>", methods=["GET"])
def get_pet(id):
    try:
        cursor = db.cursor()
        cursor.execute("SELECT * FROM pets WHERE petid = %s", (id,))
        result =cursor.fetchone()
        if not result:
            return jsonify("No pet was found"),404
        return jsonify(result), 200
        
    except Exception as e:
        return jsonify("Error"),500

@app.route("/pets/<id>", methods=["PUT"])
def update_pet(id):

        newitem = request.json
        cursor = db.cursor()
        cursor.execute("UPDATE pets SET name=%s,hunger=%s,energy=%s,mood=%s WHERE petid=%s", (newitem["name"],newitem["hunger"],newitem["energy"],newitem["mood"],id,))
        db.commit()

       
        return jsonify("Pet updated"), 200
    
    









if __name__=='__main__':
    app.run()
