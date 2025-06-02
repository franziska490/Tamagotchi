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


# Haustier füttern
@app.route("/pets/<id>/feed", methods=["POST"])
def feed_pet(id):
    try:
        cursor = db.cursor()
        cursor.execute("UPDATE pets SET hunger = LEAST(hunger + 40, 100) WHERE petid = %s", (id,))
        db.commit()
        if cursor.rowcount == 0:
            return jsonify("Feed failed – no pet with this ID"), 404
        return jsonify("Pet fed"), 200
    except Exception:
        return jsonify("Error"), 500

# Haustier spielen lassen
@app.route("/pets/<id>/play", methods=["POST"])
def play_pet(id):
    try:
        cursor = db.cursor()
        cursor.execute("UPDATE pets SET mood = LEAST(mood + 20, 100) WHERE petid = %s", (id,))
        db.commit()
        if cursor.rowcount == 0:
            return jsonify("Play failed – no pet with this ID"), 404
        return jsonify("Pet played with"), 200
    except Exception:
        return jsonify("Error"), 500

# Haustier schlafen lassen
@app.route("/pets/<id>/sleep", methods=["POST"])
def sleep_pet(id):
    try:
        cursor = db.cursor()
        cursor.execute("UPDATE pets SET energy = LEAST(energy + 30, 100) WHERE petid = %s", (id,))
        db.commit()
        if cursor.rowcount == 0:
            return jsonify("Sleep failed – no pet with this ID"), 404
        return jsonify("Pet slept"), 200
    except Exception:
        return jsonify("Error"), 500

# Registrierung
@app.route("/auth/register", methods=["POST"])
def register():
    try:
        data = request.json
        cursor = db.cursor()
        cursor.execute("INSERT INTO users (username, password, role) VALUES (%s, %s, 'user')",
                       (data["username"], data["password"]))
        db.commit()
        return jsonify("User registered"), 201
    except Exception:
        return jsonify("Error"), 500

# Login
@app.route("/auth/login", methods=["POST"])
def login():
    try:
        data = request.json
        cursor = db.cursor()
        cursor.execute("SELECT * FROM users WHERE username = %s AND password = %s",
                       (data["username"], data["password"]))
        result = cursor.fetchone()
        if not result:
            return jsonify("Login failed"), 401
        return jsonify("Login successful"), 200
    except Exception:
        return jsonify("Error"), 500









if __name__=='__main__':
    app.run()
