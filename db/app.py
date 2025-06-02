from flask import Flask, jsonify, request
import mysql.connector
import bcrypt


app = Flask(__name__)

# Datenbankverbindung
db = mysql.connector.connect(
    host="localhost",
    user="root",
    password="3172",
    database="tamagotchidb"
)

@app.route("/pets", methods=["GET"])
def get_pets():
    ownerid = request.args.get("ownerid")
    cursor = db.cursor()

    if ownerid:
        cursor.execute("SELECT * FROM pets WHERE ownerid = %s", (ownerid,))
    else:
        cursor.execute("SELECT * FROM pets")

    rows = cursor.fetchall()
    pets = []
    for row in rows:
        pets.append({
            "petid": row[0],
            "name": row[1],
            "hunger": row[2],
            "energy": row[3],
            "mood": row[4],
            "ownerid": row[5],
            "imagepath": row[6]
        })

    return jsonify(pets), 200

# Einzelnes Haustier abrufen
@app.route("/pets/<id>", methods=["GET"])
def get_pet(id):
    try:
        cursor = db.cursor()
        cursor.execute("SELECT * FROM pets WHERE petid = %s", (id,))
        result = cursor.fetchone()
        if not result:
            return jsonify("No pet was found"), 404
        return jsonify({
            "petid": result[0],
            "name": result[1],
            "hunger": result[2],
            "energy": result[3],
            "mood": result[4],
            "ownerid": result[5],
            "imagepath": result[6]
        }), 200
    except Exception:
        return jsonify("Error"), 500

# Haustier aktualisieren
@app.route("/pets/<id>", methods=["PUT"])
def update_pet(id):
    try:
        newitem = request.json
        cursor = db.cursor()
        cursor.execute(
            "UPDATE pets SET name=%s, hunger=%s, energy=%s, mood=%s, imagepath=%s WHERE petid=%s",
            (newitem["name"], newitem["hunger"], newitem["energy"], newitem["mood"], newitem["imagepath"], id)
        )
        db.commit()
        return jsonify("Pet updated"), 200
    except Exception:
        return jsonify("Error"), 500

# Feed-Action
@app.route("/pets/<id>/feed", methods=["POST"])
def feed_pet(id):
    try:
        cursor = db.cursor()
        cursor.execute("UPDATE pets SET hunger = LEAST(hunger + 40, 100) WHERE petid = %s", (id,))
        db.commit()
        if cursor.rowcount == 0:
            return jsonify("Feed failed, no pet with this ID"), 404
        return jsonify("Pet fed"), 200
    except Exception:
        return jsonify("Error"), 500

# Play-Action
@app.route("/pets/<id>/play", methods=["POST"])
def play_pet(id):
    try:
        cursor = db.cursor()
        cursor.execute("UPDATE pets SET mood = LEAST(mood + 20, 100) WHERE petid = %s", (id,))
        db.commit()
        if cursor.rowcount == 0:
            return jsonify("Play failed, no pet with this ID"), 404
        return jsonify("Pet played with"), 200
    except Exception:
        return jsonify("Error"), 500

# Sleep-Action
@app.route("/pets/<id>/sleep", methods=["POST"])
def sleep_pet(id):
    try:
        cursor = db.cursor()
        cursor.execute("UPDATE pets SET energy = LEAST(energy + 30, 100) WHERE petid = %s", (id,))
        db.commit()
        if cursor.rowcount == 0:
            return jsonify("Sleep failed, no pet with this ID"), 404
        return jsonify("Pet slept"), 200
    except Exception:
        return jsonify("Error"), 500
    
@app.route("/pets", methods=["POST"])
def create_pet():
    try:
        data = request.json
        cursor = db.cursor()
        cursor.execute("""
            INSERT INTO pets (name, hunger, energy, mood, ownerid, imagepath)
            VALUES (%s, %s, %s, %s, %s, %s)
        """, (
            data["name"], data["hunger"], data["energy"], data["mood"],
            data["ownerid"], data["imagepath"]
        ))
        db.commit()
        return jsonify("Pet created"), 201
    except Exception as e:
        print("Fehler beim Speichern:", e)
        return jsonify("Error creating pet"), 500



# Registrierung
@app.route("/auth/register", methods=["POST"])
def register():
    try:
        data = request.json
        hashed_pw = bcrypt.hashpw(data["password"].encode("utf-8"), bcrypt.gensalt())
        cursor = db.cursor()
        cursor.execute("INSERT INTO users (username, password, role) VALUES (%s, %s, 'user')",
                       (data["username"], hashed_pw))
        db.commit()
        return jsonify("User registered"), 201
    except Exception:
        return jsonify("Error"), 500


@app.route("/auth/login", methods=["POST"])
def login():
    try:
        data = request.json
        cursor = db.cursor()
        cursor.execute("SELECT password FROM users WHERE username = %s", (data["username"],))
        result = cursor.fetchone()
        if not result:
            return jsonify("Login failed"), 401

        stored_hash = result[0].encode("utf-8")
        if bcrypt.checkpw(data["password"].encode("utf-8"), stored_hash):
            return jsonify("Login successful"), 200
        else:
            return jsonify("Login failed"), 401
    except Exception:
        return jsonify("Error"), 500



if __name__ == '__main__':
    app.run()
